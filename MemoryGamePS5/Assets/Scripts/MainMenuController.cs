using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    private Button playButtonMainMenu;
    private Button loadButtonMainMenu;
    private Button optionButtonMainMenu;
    private Button quitButtonMainMenu;
    private Button backButtonOptionMenu;
    private Button backButtonLoadMenu;

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject loadMenu;

    private SaveDataContainer sceneFromSaving;

    private void Start()
    {

        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
        loadMenu.SetActive(false);

        if (mainMenu != null)
        {
            playButtonMainMenu = GameObject.Find("PlayButton").GetComponent<Button>();
            loadButtonMainMenu = GameObject.Find("LoadButton").GetComponent<Button>();
            optionButtonMainMenu = GameObject.Find("OptionsButton").GetComponent<Button>();
            quitButtonMainMenu = GameObject.Find("QuitButton").GetComponent<Button>();
        }

            
    }

    private void Update()
    {
        if (playButtonMainMenu.pressed)
        {
            // Load the last scene saved by the user
            if (System.IO.File.Exists(JSONSaving.GetPath("SaveGame.json")))
            {
                sceneFromSaving = JSONSaving.ReadFromJSON<SaveDataContainer>("SaveGame.json");

                SceneManager.LoadScene(sceneFromSaving.level);
            }
            else // Start from the first scene
            {
                SceneManager.LoadScene("Level1.1");
            }
        }

        if (loadButtonMainMenu.pressed) 
        {
            mainMenu.gameObject.SetActive(false);

            StartCoroutine(WaitABit());

            loadMenu.gameObject.SetActive(true);

            backButtonLoadMenu = GameObject.Find("/Canvas/LoadMenu/BackButton").GetComponent<Button>();

        }

        if (optionButtonMainMenu.pressed)
        {
            mainMenu.gameObject.SetActive(false);
            optionsMenu.gameObject.SetActive(true);

            backButtonOptionMenu = GameObject.Find("/Canvas/OptionsMenu/BackButton").GetComponent<Button>();

        }

        if (quitButtonMainMenu.pressed)
        {
            Debug.Log("Quit button pressed from main menu");
            Application.Quit();
        }
    

        if (backButtonOptionMenu != null)
        {
            if (backButtonOptionMenu.pressed)
            {
                mainMenu.gameObject.SetActive(true);
                optionsMenu.gameObject.SetActive(false);
                loadMenu.gameObject.SetActive(false);
            }
        }       
        
        if (backButtonLoadMenu != null)
        {
            if (backButtonLoadMenu.pressed)
            {
                mainMenu.gameObject.SetActive(true);
                optionsMenu.gameObject.SetActive(false);
                loadMenu.gameObject.SetActive(false);
            }
        }
    }


    private IEnumerator WaitABit()
    {
        yield return new WaitForSeconds(0.1f); // delay for 0.1 seconds
    }
}
