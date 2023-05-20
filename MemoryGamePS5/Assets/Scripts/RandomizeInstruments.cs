using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;


public class RandomizeInstruments : MonoBehaviour
{
    // Create a list of instruments. Each instrument contains a name, a set of melodies and a group
    public List<InstrumentContainer> instruments = new List<InstrumentContainer>();

    // Dictionary that associates a similarity group to each instrument
    Dictionary<string, int> instrumentSimilarity = new Dictionary<string, int>
        {
            { "Xylophone", 1 },
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
            { "Drum", 6 }
        };



    public Tuple<List<string>, List<string>> SelectAndRandomizeCards(int numberOfCards, bool similar, bool sameMelody)
    {

        {
            int numberOfInstruments = numberOfCards / 2;
            System.Random rnd = new System.Random();

            // Single index melody for "same melody" condition
            int idxMelodies = 0;

            List<int> melIdxs = new List<int>();
            List<string> audioClipArray = new List<string>();
            List<string> spritesArray = new List<string>();
            List<string> selectedInstruments = new List<string>(numberOfInstruments);
            List<string> selectedMelodies = new List<string>(numberOfInstruments);

            // Load folder content using Resources
            string folderPath = "Sounds/Instruments";
            AudioClip[] audioClips = Resources.LoadAll<AudioClip>(folderPath);

            // Process the loaded audio clips
            string tmpInstrument = "";
            List<string> tmpMelody = new List<string>();

            foreach (AudioClip audioClip in audioClips)
            {
                string clipName = audioClip.name;
                string[] content = clipName.Split('_');
                string instrument = content[0];
                string melody = content[1];

                if (tmpInstrument != instrument)
                {
                    tmpInstrument = instrument;
                    tmpMelody = new List<string>();
                    tmpMelody.Add(melody);
                    instruments.Add(new InstrumentContainer(folderPath, tmpInstrument, tmpMelody, instrumentSimilarity[tmpInstrument]));
                }
                else
                {
                    tmpMelody.Add(melody);
                    instruments[instruments.Count - 1].melodies = tmpMelody;
                }
            }






            // Create a list of instruments for each similarity group
            var instrumentGroups = instrumentSimilarity.GroupBy(x => x.Value).Select(x => x.Select(y => y.Key).ToList()).ToList();


            // Select instruments
            if (similar) // Select similar instruments
            {
                // Select one instrument per group
                foreach (var group in instrumentGroups)
                {
                    var instrument = group[rnd.Next(group.Count)];

                    if (instrument != "Drums")
                    {
                        selectedInstruments.Add(instrument);
                    }
                }


                // Generate a random starting index
                int startIndex = rnd.Next(selectedInstruments.Count);

                // Extract the sequence of 4 instruments
                selectedInstruments = selectedInstruments
                  .Skip(startIndex)
                  .Take(numberOfInstruments)
                  .Concat(selectedInstruments.Take(numberOfInstruments - selectedInstruments.Count + startIndex))
                  .ToList();

            }
            else // Select different instruments
            {

                // Shuffle the similarity groups
                for (int i = instrumentGroups.Count - 1; i > 0; i--)
                {
                    int j = rnd.Next(i + 1);
                    var temp = instrumentGroups[i];
                    instrumentGroups[i] = instrumentGroups[j];
                    instrumentGroups[j] = temp;
                }


                int idx = 0;

                // Select numberOfCards instruments
                foreach (var group in instrumentGroups)
                {
                    if (idx == numberOfInstruments)
                    {
                        break;
                    }

                    idx++;

                    var instrument = group[rnd.Next(group.Count)];
                    selectedInstruments.Add(instrument);
                }

            }


            // Select melodies
            if (sameMelody) // Pick up a random melody for all instruments
            {
                idxMelodies = rnd.Next(instruments[0].melodies.Count);

                // For every instrument
                for (int i = 0; i < numberOfInstruments; i++)
                {
                    // Find the corresponding index in the list of all instruments
                    int idxInstrument = instruments.FindIndex(x => x.instrument == selectedInstruments[i]);
                    // Save the index in a list
                    selectedMelodies.Add(instruments[idxInstrument].melodies[idxMelodies]);
                }

            }
            else
            {
                // For all the cards
                while (melIdxs.Count < numberOfInstruments)
                {
                    // Generate a random number between 0 and maximum number of melodies
                    int randInt = rnd.Next(0, instruments[0].melodies.Count - 1);

                    // Add the number to the list if it's not already contained
                    if (!melIdxs.Contains(randInt))
                    {
                        melIdxs.Add(randInt);
                    }

                }

                // For every instrument
                for (int i = 0; i < numberOfInstruments; i++)
                {
                    // Find the corresponding index in the list of all instruments
                    int idxInstrument = instruments.FindIndex(x => x.instrument == selectedInstruments[i]);

                    // Save the index in a list
                    selectedMelodies.Add(instruments[idxInstrument].melodies[melIdxs[i]]);
                }

            }



            for (int entry = 0; entry < selectedMelodies.Count; entry++)
            {
                string soundPathFile = Path.Combine("Sounds", "Instruments", selectedInstruments[entry] + "_" + selectedMelodies[entry]);
                //string soundPathFile = "Sounds/Instruments/" + selectedInstruments[entry] + "_" + selectedMelodies[entry];
                audioClipArray.Add(soundPathFile);

                string imagePathFile = Path.Combine("Sprites", "Cards", "Card" + selectedInstruments[entry]);
                //string imagePathFile = "Images/Cards/" + "Card" + selectedInstruments[entry];
                spritesArray.Add(imagePathFile);

            }

            // Print all selected audioclips and sprites
            //audioClipArray.ForEach(Debug.Log);
            //spritesArray.ForEach(Debug.Log);

            return Tuple.Create(audioClipArray, spritesArray);
        }
    }
}
