using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using System.Text;
using System;



[RequireComponent(typeof(AudioSource))]
public class Sound: MonoBehaviour
{
    AudioClip myAudioClip;
    public Config config;

    void Start() { }
    void Update() { }

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 60, 50), "Record"))
        {
            myAudioClip = Microphone.Start(null,false,config.sound_limit, 44100);
            
        }

        if (GUI.Button(new Rect(10, 70, 60, 50), "Save"))
        {
            //myAudioClip = SavWav.TrimSilence(myAudioClip, 50.0f);
            SavWav.Save("myfile", myAudioClip); // error
        }

        if (GUI.Button(new Rect(10, 130, 60, 50), "Play"))
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.clip = myAudioClip;
            audio.Play();
        }
      
    }

    


}


