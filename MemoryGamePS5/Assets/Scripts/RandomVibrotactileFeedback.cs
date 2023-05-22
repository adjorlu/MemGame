using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomVibrotactileFeedback : MonoBehaviour
{
    // Array with status of vibrotactile feedback for each level
    //private VibrotactileContainer vibrotactileContainer = new VibrotactileContainer(new BitArray(16, false));
    private VibrotactileContainer vibrotactileContainer = new VibrotactileContainer(new bool[16]);
    System.Random randomIdx = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        if (!System.IO.File.Exists(JSONSaving.GetPath("LevelRandomVibrotactile.json")))
        {
            RandomizeVibrotactileFeedbackLevels(); // Create and save a vector for muting/unmuting vibrotactile feedback for each 
        }


    }


    public void RandomizeVibrotactileFeedbackLevels ()
    {
        // Randomize true/false pairwise for all the vector
       for (int i = 0; i < vibrotactileContainer.randomVibrotactileLevels.Length; i += 2)
        {
            int randomIndex = randomIdx.Next(i, i + 2); // Generate a random index between i (inclusive) and i + 2 (exclusive)
            vibrotactileContainer.randomVibrotactileLevels[randomIndex] = true;
        }

        JSONSaving.SaveToJSON<VibrotactileContainer>(vibrotactileContainer, "LevelRandomVibrotactile.json"); // Save it in a file
    }    
}
