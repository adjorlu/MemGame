using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{

    [SerializeField] Button thisButton;
    int thisLevel;


    public void UpdateLabel()
    {
        string thisLabel = this.GetComponentInChildren<TextMeshProUGUI>().text;

        string[] splitLabel = thisLabel.Split(new char[] { ' ' });

        thisLevel = Int32.Parse(splitLabel[1]);
    }

    private void Update()
    {
        if (thisButton.pressed)
        {
            SceneManager.LoadScene(thisLevel);
        }
            
    }

}
