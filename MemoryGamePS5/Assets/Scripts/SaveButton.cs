using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveButton : MonoBehaviour
{
    //private SaveDataContainer sceneToSave; 

    public static void SaveGame() // Save level 
    {
        Debug.Log("Save Pressed");

        SaveDataContainer sceneToSave;

        sceneToSave = new SaveDataContainer(SceneManager.GetActiveScene().buildIndex);

        JSONSaving.SaveToJSON<SaveDataContainer>(sceneToSave, "SaveGame.json");
    }
}
