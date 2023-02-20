using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class JSONInputHandler : MonoBehaviour
{
    [SerializeField] string filename;

    List<DataCollector> entries = new List<DataCollector>();

    private void Start()
    {
        entries = JSONSaving.ReadListFromJSON<DataCollector>(filename);
    }

    public void AddNameToList(string name, float clicks, int level)
    {
        entries.Add(new DataCollector(name, clicks, level));

        JSONSaving.SaveToJSON<DataCollector>(entries, filename);
    }
}