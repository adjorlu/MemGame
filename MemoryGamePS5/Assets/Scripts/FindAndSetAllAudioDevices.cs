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


    private GetSelectedAudioDevice getSelectedAudioDevice;

    void Start()
    {
        getSelectedAudioDevice = GetComponent<GetSelectedAudioDevice>();
    }

    public void UpdateAllAudioDevicesID()
    {
        getSelectedAudioDevice = GetComponent<GetSelectedAudioDevice>();

        // Find all the GameObjects with the name AudioPC
        GameObject[] audioPCObjects = GameObject.FindObjectsOfType<GameObject>().Where(obj => obj.name == "AudioPC").ToArray();

        // For each AudioPC change its outputdriver
        foreach (GameObject audioPCObject in audioPCObjects)
        {
            //var audioSourceOutputDevice = audioPCObject.GetComponent<AudioSourceOutputDevice>();

            if (audioPCObject.TryGetComponent<AudioSourceOutputDevice>(out var audioSourceOutputDevice))
            {
                getSelectedAudioDevice.SetOutputDriverID(audioSourceOutputDevice); // This gives a NullReference but I don't understand why. 
            }
            else
            {
                Debug.LogError("AudioSourceOutputDevice component not found on the AudioPC object!");
            }
        }
    }
 
}
