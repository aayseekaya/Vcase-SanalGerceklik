using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.script {

    public class CasesScript : MonoBehaviour {


       public Config config;

        public PanelList panel_list;

        public List<Case> cases;      

        void Start()
        {
            string url = config.API_address + "/vcase_api/cases/";
            WWWForm form = new WWWForm();
            Dictionary<string, string> requestHeader = form.headers;

            if (requestHeader.ContainsKey("Authorization"))
                requestHeader["Authorization"] = "Token " + "1a4d35fff6b1178d4254884cf31a139539667a71";

            else
                requestHeader.Add("Authorization", "Token " + "1a4d35fff6b1178d4254884cf31a139539667a71");

            WWW www = new WWW(url, null, requestHeader);
            StartCoroutine(WaitForRequest(www));

            Camera.main.transform.position = new Vector3(22, -2, 12);




        } 

        IEnumerator WaitForRequest(WWW www)
        {
            yield return www;

            // check for errors
            if (www.error == null)
            {
                Debug.Log("WWW Result!: " + www.text);// contains all the data sent from the server


                cases = getJsonArray<Case>(www.text);
                panel_list.drawPanels(cases);

            }
            else
            {
                Debug.Log("WWW Error: " + www.error);
            }
        }

        public static List<T> getJsonArray<T>(string json)
        {
            string newJson = "{ \"array\": " + json + "}";
            VCaseList<T> wrapper = JsonUtility.FromJson<VCaseList<T>>(newJson);
            return wrapper.array;
        }
    }
}