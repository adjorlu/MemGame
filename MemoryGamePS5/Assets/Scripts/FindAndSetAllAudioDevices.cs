using AudioStream;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FindAndSetAllAudioDevices : MonoBehaviour
{
    GetSelectedAudioDevice getSelectedAudioDevice;
    public void UpdateAllAudioDevicesID ()
    {

        GameObject[] audioPCObjects = GameObject.FindObjectsOfType<GameObject>().Where(obj => obj.name == "AudioPC").ToArray();

        foreach (GameObject audioPCObject in audioPCObjects)
        {
            //getSelectedAudioDevice.SetOutputDriverID(audioPCObject.GetComponent<AudioSourceOutputDevice> );
        }

    }
}
