using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class RandomizeInstruments : MonoBehaviour
{

    // Create a list of instruments. Each instrument contains a name, a set of melodies and a group
    public List<InstrumentContainer> instruments = new List<InstrumentContainer>();


    void Start()
    {
        string tmpInstrument = "";
        List<string> tmpMelody = new List<string>();
        char[] wavChar = {'.', 'w', 'a', 'v'};
        int idx = 0;

        // Load folder content
        string folderPath = "Assets/Sounds/Instruments";

        // Check if the folder exists
        if (Directory.Exists(folderPath))
        {
            // Get all files and directories in the folder
            string[] files = Directory.GetFiles(folderPath);
            
            // For each element in the folder
            foreach(string file in files)
            {
                // If it's a .wav file
                if (!file.Contains(".meta") & file.Contains(".wav"))
                {
                    // Split instrument name and melody name
                    string[] content = file.Split("\\");
                    content = content[1].Split('_');

                    // If it's a new instrument
                    if (tmpInstrument != content[0])
                    {
                       
                        if(idx != 0)
                        {
                            // Save the data in a new InstrumentContainer class
                            instruments.Add(new InstrumentContainer(tmpInstrument, tmpMelody, AssignSimilarityGroup(tmpInstrument)));
                            tmpMelody.Clear();
                        }

                        // Save instrument
                        tmpInstrument = content[0];
                        // Save first melody
                        tmpMelody.Add(content[1].TrimEnd(wavChar));
                    }
                    else // If it's not a new intrument
                    {
                        // Add an element to tmpMelody and save
                        tmpMelody.Add(content[1].TrimEnd(wavChar));
                    }

                    
                }
                else  // Otherwise skip the file
                { 
                    continue;
                }     
                
                idx++;

            }

            

        }
        else
        {
            Debug.LogError("Folder does not exist: " + folderPath);
        }


        Debug.Log("");
    }


    private int AssignSimilarityGroup(string instrumentName)
    {
        Dictionary<string, int> instrumentSimilarity = new Dictionary<string, int>
        {
            { "Piano", 1 },
            { "Guitar", 1 },
            { "Bass", 2 },
            { "Cello", 3 },
            { "Viola", 3 },
            { "Violin", 3 },
            { "Trumpet", 4 },
            { "Trombone", 4 },
            { "Flute", 5 },
            { "Sax", 5 },
            { "Clarinet", 5 },
            { "Xylophone", 6 },
            { "Drums", 7 }
        };

        return instrumentSimilarity[instrumentName];
    }    


}
