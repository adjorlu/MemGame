using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioStream;
using TMPro;

public class PopulateAudioDevices : MonoBehaviour
{

    private List<FMOD_SystemW.OUTPUT_DEVICE> availableOutputs = new List<FMOD_SystemW.OUTPUT_DEVICE>();
    public AudioSourceOutputDevice outputDevice;

    [SerializeField] private TMP_Dropdown selectAudioDeviceDropdown;



    void Start()
    {
        List<string> outputNamesDevices = GetAudioDeviceNames();

        selectAudioDeviceDropdown.ClearOptions();

        foreach (string deviceName in outputNamesDevices)
        {
            print($"Device: {deviceName}");
            selectAudioDeviceDropdown.options.Add(new TMP_Dropdown.OptionData() { text = deviceName });
        }
    }

    private List<string> GetAudioDeviceNames()
    {
        this.availableOutputs = FMOD_SystemW.AvailableOutputs(this.outputDevice.logLevel, this.outputDevice.gameObject.name, this.outputDevice.OnError);

        List<string> outputNames = new List<string>();

        for (int i = 0; i < this.availableOutputs.Count; ++i)
            outputNames.Add(this.availableOutputs[i].id + " : " + this.availableOutputs[i].name);

        return outputNames;
    }
}
