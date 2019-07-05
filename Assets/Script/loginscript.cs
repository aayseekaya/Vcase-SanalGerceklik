using Assets.script;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class loginScript : MonoBehaviour
{
    public Config config;

   
    public InputField usrname;
    public InputField passwrd;
    public Button loginButton;
    public Text warning;

  public  CarrierScript carrier;

    public GameObject carrierObject;

    public string nextScene;

    UserJsonList userlist = new UserJsonList();
    private string POSTWishlistGetURL;


    public UserInformation userinformation;


    // Use this for initialization
    void Start()
    {
        POSTWishlistGetURL = config.API_address + "/gettoken/";
        passwrd.contentType = InputField.ContentType.Password;
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
           warning.text = "İnternet bağlanıp tekrar deneyiniz!";
        }

    }


    public void OnSubmit()
    {
        userlist.username = usrname.text;
        userlist.password = passwrd.text;
        loginButton.enabled = false;
        Post();
    }

    public WWW Post()
    {
        WWWForm form = new WWWForm();
        // Convert to JSON (and to bytes)
        string playerToJason = JsonUtility.ToJson(userlist, true);
        byte[] postData = System.Text.Encoding.ASCII.GetBytes(playerToJason);

        Dictionary<string, string> postHeader = form.headers;
        if (postHeader.ContainsKey("Content-Type"))
            postHeader["Content-Type"] = "application/json";
        else
            postHeader.Add("Content-Type", "application/json");

        //if (postHeader.ContainsKey("Authorization"))
        //    postHeader["Authorization"] = "Token " + "1a4d35fff6b1178d4254884cf31a139539667a71" ;
        //else
        //    postHeader.Add("Authorization", "Token " + "1a4d35fff6b1178d4254884cf31a139539667a71");

        WWW www = new WWW(POSTWishlistGetURL, postData, postHeader);
        StartCoroutine(WaitForRequest(www));
        return www;

    }

    IEnumerator WaitForRequest(WWW data)
    {
        yield return data;

        if (data.error != null)
        {
            Debug.Log("There was an error sending request: " + data.error);
            


            warning.text =data.error;
            loginButton.enabled = true;
        }
        else
        {
            Debug.Log(data.text);
           
           

            userinformation = getJsonUser(data.text);

            Debug.Log(userinformation.user_id);
            Debug.Log(userinformation.email);
            Debug.Log(userinformation.token);

            carrier.config = this.config;
            carrier.userId = userinformation.user_id;
            carrier.userMail = userinformation.email;
            carrier.Token = userinformation.token;

            StartCoroutine(LoadYourAsyncScene());


        }

    }

    IEnumerator LoadYourAsyncScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        SceneManager.MoveGameObjectToScene(carrierObject, SceneManager.GetSceneByName(nextScene));
        SceneManager.UnloadSceneAsync(currentScene);
        loginButton.enabled = true;
    }


    public static UserInformation getJsonUser(string json)
    {
        string newJson =json ;
        UserInformation wrapper = JsonUtility.FromJson<UserInformation>(newJson);
        return wrapper;
    }

}
