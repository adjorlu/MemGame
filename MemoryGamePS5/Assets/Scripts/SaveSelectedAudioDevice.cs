using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSelectedAudioDevice : MonoBehaviour
{
    public void SaveAudioDevice(int idx)
    {
        print($"Saved device: {idx}");
        PlayerPrefs.SetFloat("audioDeviceIndex", idx);
    }
}

