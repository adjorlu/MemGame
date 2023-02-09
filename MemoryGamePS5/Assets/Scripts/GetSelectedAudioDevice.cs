using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioStream;

public class GetSelectedAudioDevice : MonoBehaviour
{
    int outputDriver;
    public AudioSourceOutputDevice outputDevice;

    //private PopulateAudioDevices audioDevices;

    //List<string> listOfDevices;

    // Start is called before the first frame update
    void Start()
    {
        outputDriver = (int)PlayerPrefs.GetFloat("audioDeviceIndex");

        //listOfDevices = audioDevices.GetAudioDeviceNames();

        //// Check if the saved audio devices is exceeding the number of available devices
        //if (listOfDevices.Count < outputDriver)
        //{
        //    outputDriver = 0;
        //}

        outputDevice.SetOutput(outputDriver);
    }

}
