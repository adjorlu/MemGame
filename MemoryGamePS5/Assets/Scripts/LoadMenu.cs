using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMenu : MonoBehaviour
{
    [SerializeField] private GameObject deleteMenu;
    [SerializeField] private GameObject levelsMenu;


    private SaveDataContainer sceneFromSaving;

    private Button deleteButton;
    private Button backButtonLoadMenu;

    private Button yesButton;
    private Button noButton;

    // Start is called before the first frame update
    void Start()
    {
        deleteButton = GameObject.FindGameObjectWithTag("DeleteButton").GetComponent<Button>();
        GameObject[] buttons = GameObject.FindGameObjectsWithTag("LevelButton");

        deleteMenu.SetActive(false);
        yesButton = GameObject.Find("/Canvas/LoadMenu/DeleteMenu/Yes").GetComponent<Button>();
        noButton = GameObject.Find("/Canvas/LoadMenu/DeleteMenu/No").GetComponent<Button>();


        backButtonLoadMenu = GameObject.Find("/Canvas/LoadMenu/LevelsMenu/BackButton").GetComponent<Button>();

        int cnt = 1;

        // Be sure that all buttons are not active and add labels to them
        foreach (GameObject button in buttons)
        {
            button.GetComponentInChildren<TextMeshProUGUI>().text = "LEVEL " + cnt.ToString();
            cnt++;

            button.GetComponent<LevelButton>().UpdateLabel();
        }    
        

        // Get the total number of levels in the project (remove main menu from all scenes)
        int sceneCount = SceneManager.sceneCountInBuildSettings - 1;

        if (System.IO.File.Exists(JSONSaving.GetPath("SaveGame.json")))
        {
            // Get the saved level
            sceneFromSaving = JSONSaving.ReadFromJSON<SaveDataContainer>("SaveGame.json");

            // Activate all levels up to the last played
            for (int i = 0; i < sceneFromSaving.level; i++)
            {
                buttons[i].SetActive(true);
            }

            // Deactivate all the rest
            for (int i = sceneFromSaving.level; i < buttons.Length; i++)
            {
                buttons[i].SetActive(false);
            }
        }
        else
        {

            buttons[0].SetActive(true); // Activate only first button

            // Be sure the all the others are not active
            for (int i = 1; i < buttons.Length; i++)
            {
                buttons[i].SetActive(false);
            }
        }

    }

    void Update()
    {
        if (deleteButton.pressed)
        {
            levelsMenu.SetActive(false);
            deleteMenu.gameObject.SetActive(true);
        }

        if (yesButton.pressed)
        {
            DeleteAllScenes();
            deleteMenu.gameObject.SetActive(false);
            levelsMenu.SetActive(true);
            Start();

        }

        if (noButton.pressed)
        {
            deleteMenu.gameObject.SetActive(false);
            levelsMenu.SetActive(true);
        }
    }

    void DeleteAllScenes()
    {
        SaveDataContainer startFromFirst;
        startFromFirst = new SaveDataContainer(1);

        JSONSaving.SaveToJSON<SaveDataContainer>(startFromFirst, "SaveGame.json");
    }
}
