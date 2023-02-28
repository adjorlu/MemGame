using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioStream;

public class GetSelectedAudioDevice : MonoBehaviour
{
    int outputDriver;
    public AudioSourceOutputDevice outputDevice;

    void Start()
    {
        SetOutputDriverID(outputDevice);
    }


    public void SetOutputDriverID(AudioSourceOutputDevice outputDevice)
    {
        outputDriver = (int)PlayerPrefs.GetFloat("audioDeviceIndex");
        if (outputDevice.ready)
        {
            outputDevice.SetOutput(outputDriver);
        }
        else
        {
            print($"Device: {outputDevice.name} not ready!");
        }
    }
}
