﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class SceneController : MonoBehaviour
{
    public int gridRows = 2;
    public int gridCols = 4;
    public float offsetX = 2f;
    public float offsetY = 2.5f;
    public int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3 };

    [SerializeField] MemoryCard originalCard;
    [SerializeField] Sprite[] images;
    [SerializeField] AudioClip[] sounds;
    [SerializeField] TMP_Text scoreLabel;
    [SerializeField] TMP_Text rewardLabel;
    [SerializeField] AudioClip scoreAudio;
    [SerializeField] GameObject panelBackground;
    public AudioSource UIAudio;

    private MemoryCard firstRevealed;
    private MemoryCard secondRevealed;
    private int score = 0;
    private int currentScene;
    private List<int> matchId = new List<int>();

    private List<DataCollector> dataCollector = new List<DataCollector>();

    public bool canReveal
    {
        get { return secondRevealed == null; }
    }

    // Use this for initialization
    void Start()
    {
        // be sure that the label is disabled at the beginning of the level
        rewardLabel.enabled = false;
        panelBackground.SetActive(false);

        currentScene = SceneManager.GetActiveScene().buildIndex;

        Debug.Log("Scene number is: " + currentScene);
        Vector3 startPos = originalCard.transform.position;

        // create shuffled list of cards
        numbers = ShuffleArray(numbers);

        // place cards in a grid
        for (int i = 0; i < gridCols; i++)
        {
            for (int j = 0; j < gridRows; j++)
            {
                MemoryCard card;

                // use the original for the first grid space
                if (i == 0 && j == 0)
                {
                    card = originalCard;
                }
                else
                {
                    card = Instantiate(originalCard) as MemoryCard;
                }

                // next card in the list for each grid space
                int index = j * gridCols + i;
                int id = numbers[index];
                card.SetCard(id, images[id]);
                card.SetAudio(id, sounds[id]);

                float posX = (offsetX * i) + startPos.x;
                float posY = -(offsetY * j) + startPos.y;
                card.transform.position = new Vector3(posX, posY, startPos.z);
            }
        }

        // load the clip
        UIAudio.clip = scoreAudio;

    }

    // everyday im Knuth shuffling 
    private int[] ShuffleArray(int[] numbers)
    {
        int[] newArray = numbers.Clone() as int[];
        for (int i = 0; i < newArray.Length; i++)
        {
            int tmp = newArray[i];
            int r = UnityEngine.Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }
        return newArray;
    }

    public void CardRevealed(MemoryCard card)
    {
        if (firstRevealed == null)
        {
            firstRevealed = card;
        }
        else
        {
            secondRevealed = card;
            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch()
    {

        // increment score if:
        // - the cards match
        // - the selected cards are not the same (not same position)
        // - the couple selected have not been selected before
        if (firstRevealed.Id == secondRevealed.Id &&
            firstRevealed.transform.position != secondRevealed.transform.position &&
            !matchId.Contains(firstRevealed.Id))
        {

            // add the found match pair
            matchId.Add(firstRevealed.Id);

            // increment score
            score++;
            scoreLabel.text = $"SCORE: {score}";

            print($"SCORE: {score}");

            // show the instrument 
            firstRevealed.Reveal();
            secondRevealed.Reveal();

            // correct match audio feedback
            firstRevealed.alreadyMatched = true;
            secondRevealed.alreadyMatched = true;

            firstRevealed.Matched();
            secondRevealed.Matched();

            new WaitForSeconds(1f);
            UIAudio.Play();

            // if level is completed
            if (score == (gridCols * gridRows) / 2)
            {
                yield return new WaitForSeconds(0.5f);
                panelBackground.SetActive(true);

                // Stop sound and haptics from the cards
                firstRevealed.computerAudio.Stop();                
                firstRevealed.GetComponent<AudioSource>().Stop();

                secondRevealed.computerAudio.Stop();
                secondRevealed.GetComponent<AudioSource>().Stop();

                yield return new WaitForSeconds(0.5f);
                rewardLabel.enabled = true;

                yield return new WaitForSeconds(3.0f);
                ChangeScene();
            }
        }

        // otherwise turn them back over after .5s pause
        else
        {
            yield return new WaitForSeconds(.2f);

            if (!matchId.Contains(firstRevealed.Id))
            {
                firstRevealed.Unreveal();
                secondRevealed.Unreveal();
            }
        }

        firstRevealed = null;
        secondRevealed = null;
    }

    public void Restart()
    {
        SceneManager.LoadScene("Scene");
    }

    private void ChangeScene()
    {
        CollectInfo();
        SceneManager.LoadScene(currentScene + 1);
    }

    private void CollectInfo()
    {
        var allCards = FindObjectsOfType<MemoryCard>();

        DateTime localDate = DateTime.Now;

        print(localDate.ToString("yyyyMMddHHmmss"));

        foreach (var card in allCards)
        {

            dataCollector.Add(new DataCollector(images[card.Id].name.ToString(), card.numClicks, currentScene));
            
        }

        JSONSaving.SaveToJSON<DataCollector>(dataCollector, localDate.ToString("yyyyMMddHHmmss") + "_" + currentScene.ToString() + ".json");

    }

}