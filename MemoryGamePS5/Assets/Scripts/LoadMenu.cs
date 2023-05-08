using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMenu : MonoBehaviour
{

    private SaveDataContainer sceneFromSaving;

    // Start is called before the first frame update
    void Start()
    {
        
        GameObject[] buttons = GameObject.FindGameObjectsWithTag("LevelButton");

        int cnt = 1;

        // Be sure that all buttons are not active and add labels to them
        foreach (GameObject button in buttons)
        {
            button.SetActive(false);

            button.GetComponentInChildren<TextMeshProUGUI>().text = "LEVEL " + cnt.ToString();
            cnt++;

            button.GetComponent<LevelButton>().UpdateLabel();
        }    
        

        // Get the total number of levels in the project (remove main menu from all scenes)
        int sceneCount = SceneManager.sceneCountInBuildSettings - 1;

        // Get the saved level
        sceneFromSaving = JSONSaving.ReadFromJSON<SaveDataContainer>("SaveGame.json");

        if (sceneFromSaving != null)
        {
            // Activate all levels up to the last played
            for (int i = 0; i < sceneFromSaving.level; i++)
            {
                buttons[i].SetActive(true);
            }
        }
        else
        {
            buttons[0].SetActive(true); // Activate only first button
        }

    }
}
