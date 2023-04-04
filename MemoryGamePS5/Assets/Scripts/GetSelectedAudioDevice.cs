using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioStream;

public class GetSelectedAudioDevice : MonoBehaviour
{
    private int outputDriver;
    public AudioSourceOutputDevice outputDevice;

    void Start()
    {
        StartCoroutine(SetOutputDriverID(outputDevice));
    }


    IEnumerator SetOutputDriverID(AudioSourceOutputDevice outputDevice)
    {
        yield return new WaitUntil(() => outputDevice.ready);

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
