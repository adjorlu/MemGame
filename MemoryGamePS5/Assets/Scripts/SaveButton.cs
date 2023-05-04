using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveButton : MonoBehaviour
{
    private SaveDataContainer sceneToSave; 

    public void SaveGame() // Save level 
    {
        Debug.Log("Save Pressed");

        sceneToSave = new SaveDataContainer(SceneManager.GetActiveScene().buildIndex);

        JSONSaving.SaveToJSON<SaveDataContainer>(sceneToSave, "SaveGame.json");
    }
}
