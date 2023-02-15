using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JSONSaving : MonoBehaviour
{

    private string path = "";
    private string persistentPath = "";

    private void Start()
    {
        SetPaths();
    }

    private void SetPaths()
    {
        path = Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.json";
        persistentPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "SaveData.json";
    }

    public void SaveData(DataCollector playerData)
    {
        SetPaths();
        string savePath = path;

        print ("Saving data at " + savePath);
        string json = JsonUtility.ToJson(playerData, true);
        print(json);

        using StreamWriter writer = new StreamWriter(savePath);
        writer.Write(json); 
    }

    public void LoadData()
    {
        using StreamReader reader = new StreamReader(path);
        string json = reader.ReadToEnd();

        DataCollector dataLoaded = JsonUtility.FromJson<DataCollector>(json);
        Debug.Log(dataLoaded.ToString());
    }
}
