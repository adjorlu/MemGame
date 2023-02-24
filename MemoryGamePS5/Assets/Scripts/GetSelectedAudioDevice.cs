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
        outputDriver = (int)PlayerPrefs.GetFloat("audioDeviceIndex");

        outputDevice.SetOutput(outputDriver);
    }

}
