using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSelectedAudioDevice : MonoBehaviour
{
    public void SaveAudioDevice(int idx)
    {
        print($"Selected device: {idx}");
        PlayerPrefs.SetFloat("audioDeviceIndex", idx);
    }
}

