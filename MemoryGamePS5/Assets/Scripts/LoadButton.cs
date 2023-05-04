using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadButton : MonoBehaviour
{

    private SaveDataContainer sceneFromSaving;
    public void LoadGame() // Load level
    {
        if (System.IO.File.Exists(JSONSaving.GetPath("SaveGame.json")))
        {
            
            sceneFromSaving = JSONSaving.ReadFromJSON<SaveDataContainer>("SaveGame.json");
            
            Debug.Log("Load Pressed");

            SceneManager.LoadScene(sceneFromSaving.level);

        }
    }
}
