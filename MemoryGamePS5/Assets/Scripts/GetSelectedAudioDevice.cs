using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioStream;

public class GetSelectedAudioDevice : MonoBehaviour
{
    int outputDriver;
    public AudioSourceOutputDevice outputDevice;

    // Start is called before the first frame update
    void Start()
    {
        outputDriver = ((int)PlayerPrefs.GetFloat("audioDeviceIndex"));
        outputDevice.SetOutput(outputDriver);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
