using AudioStream;
using AudioStreamSupport;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class FindAndSetAllAudioDevices : MonoBehaviour
{
    //private GetSelectedAudioDevice getSelectedAudioDevice;

    //public void UpdateAllAudioDevicesID ()
    //{
    //    // Find all the GameObjects with the name AudioPC
    //    GameObject[] audioPCObjects = GameObject.FindObjectsOfType<GameObject>().Where(obj => obj.name == "AudioPC").ToArray();

    //    // For each AudioPC change its outputdriver
    //    foreach (GameObject audioPCObject in audioPCObjects)
    //    {
    //        getSelectedAudioDevice.SetOutputDriverID(audioPCObject.GetComponent<AudioSourceOutputDevice>());
    //    }

    //}


    public void UpdateAllAudioDevicesID()
    {

        // Find all the GameObjects with the tag AudioPC
        GameObject[] audioPCObjects = GameObject.FindGameObjectsWithTag("AudioPC");
        // Find the GameObject with the tag AudioUI
        GameObject audioUIObject = GameObject.FindGameObjectWithTag("AudioUI");

        // Set the new outputdriver for AudioUI from the PlayerPrefs
        if (audioUIObject.TryGetComponent<AudioSourceOutputDevice>(out var audioSourceOutputDevice1))
        {
            audioSourceOutputDevice1.SetOutput((int)PlayerPrefs.GetFloat("audioDeviceIndex"));
        }
        else
        {
            Debug.LogError("AudioSourceOutputDevice component not found on the AudioPC object!");
        }


        // For each AudioPC change its outputdriver from the PlayerPrefs
        foreach (GameObject audioPCObject in audioPCObjects)
        {

            if (audioPCObject.TryGetComponent<AudioSourceOutputDevice>(out var audioSourceOutputDevice))
            {
                
                audioSourceOutputDevice.SetOutput((int)PlayerPrefs.GetFloat("audioDeviceIndex"));
            }
            else
            {
                Debug.LogError("AudioSourceOutputDevice component not found on the AudioPC object!");
            }
        }
    }
 
}
