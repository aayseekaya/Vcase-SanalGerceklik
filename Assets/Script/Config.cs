using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Config : MonoBehaviour {

    public int sound_limit=10;
    public string API_address = "http://192.168.1.63:8000";

    public static string[] config_keys = { "sound_limit", " API_address" };

    void Start()
    {    

        string path = @"config.txt";
        

        // This text is added only once to the file.
        if (!File.Exists(path))
        {
            // Create a file to write to.
            string createText = "sound_limit=50" + Environment.NewLine +
                "API_address=http://192.168.1.63:8000" + Environment.NewLine;
            File.WriteAllText(path, createText);
        }
        
        string[] readText = File.ReadAllLines(path);


        foreach (string s in readText)
        {
         
            string[] linesInFile = s.Split('=');
            for(int i=0;i<linesInFile.Length;i++)
            {
               
                switch (linesInFile[0])
                {
                    case "sound_limit":
                        sound_limit =int.Parse(linesInFile[1]);
                         break;

                    case "API_address":
                        API_address = linesInFile[1];
                        break;

                    default:
                        break;

                }
            }

        }

    }
}
