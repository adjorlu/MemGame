using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using System.Linq;
using System.IO;

public class SceneController : MonoBehaviour
{

    public int numCards = 8;

    private float offsetX = 2f;
    private float offsetY = 2.5f;
    
    private int gridRows = 2;
    private int gridCols = 4;

    public bool similarCards = false;
    public bool sameMelody = false;

    [SerializeField] MemoryCard originalCard;
    //[SerializeField] Sprite[] images;
    //[SerializeField] AudioClip[] sounds;
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

    // List for saving user interaction
    private List<DataCollector> dataCollector = new List<DataCollector>();

    // List of sprites and clips to load per every card
    private List<string> sprites = new List<string>();
    private List<string> audioclips = new List<string>();


    public bool canReveal
    {
        get { return secondRevealed == null; }
    }

    // Use this for initialization
    void Start()
    {
        // Be sure that the label is disabled at the beginning of the level
        rewardLabel.enabled = false;
        panelBackground.SetActive(false);

        // Create a shuffled array of cards
        int[] cardsIdexes = GenerateCardVector(numCards);

        currentScene = SceneManager.GetActiveScene().buildIndex;

        Debug.Log("Scene number is: " + currentScene);

        
        //Get set of audioclips and sprits randomly generated following the rules
        (audioclips, sprites)= GetComponent<RandomizeInstruments>().SelectAndRandomizeCards(numCards, similarCards, sameMelody);

        // Adapt the columns to the number of cards
        gridCols = numCards / gridRows;

        // Displace the first card accordingly to the total number of cards
        originalCard.transform.position = new Vector3(-(gridCols - 1), 1, 0);

        Vector3 startPos = originalCard.transform.position;

        // Place cards in a grid
        for (int i = 0; i < gridCols; i++)
        {
            for (int j = 0; j < gridRows; j++)
            {
                MemoryCard card;

                // Use the original for the first grid space
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
                int id = cardsIdexes[index];

                //card.SetCard(id, images[id]);
                //card.SetAudio(id, sounds[id]);

                card._id = id;

                // Load the sprite from the asset path and assign it to the SpriteRenderer
                card.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(sprites[id]);

                // Load the audio clip from the asset path and assign it to the AudioSource
                card.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>(audioclips[id]);
                card.computerAudio.clip = Resources.Load<AudioClip>(audioclips[id]);

                float posX = (offsetX * i) + startPos.x;
                float posY = -(offsetY * j) + startPos.y;
                card.transform.position = new Vector3(posX, posY, startPos.z);
            }
        }

        // Load the feedback audioclip for correct match
        UIAudio.clip = scoreAudio;

    }

    private int[] GenerateCardVector(int nCards)
    {
        int cardIdx = 0;

        // Empty vector with nCards
        int[] cardsVector = new int[nCards];

        // For each card
        for (int card = 0; card < cardsVector.Length; card++)
        {
            // Assign an increasing index for each couple of cards
            if (card % 2 == 0 && card > 1)
            {
                cardIdx++;

            }

            cardsVector[card] = cardIdx;
        }

        // Create shuffled list of cards
        cardsVector = ShuffleArray(cardsVector);

        return cardsVector;
    }

    // Everyday im Knuth shuffling 
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

        // Increment score if:
        // - the cards match
        // - the selected cards are not the same (not same position)
        // - the couple selected have not been selected before
        if (firstRevealed.Id == secondRevealed.Id &&
            firstRevealed.transform.position != secondRevealed.transform.position &&
            !matchId.Contains(firstRevealed.Id))
        {

            // Add the found match pair
            matchId.Add(firstRevealed.Id);

            // Increment score
            score++;
            scoreLabel.text = $"SCORE: {score}";

            print($"SCORE: {score}");

            // Show the instrument 
            firstRevealed.Reveal();
            secondRevealed.Reveal();

            // Correct match audio feedback
            firstRevealed.alreadyMatched = true;
            secondRevealed.alreadyMatched = true;

            firstRevealed.Matched();
            secondRevealed.Matched();

            new WaitForSeconds(1f);
            UIAudio.Play();

            // If level is completed
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

        // Otherwise turn them back over after .5s pause
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
        CollectCardInfo();
        SceneManager.LoadScene(currentScene + 1);
    }

   
    private void CollectCardInfo()  // Collect date, card name, clicks and compose JSON file
    {
        var allCards = FindObjectsOfType<MemoryCard>();

        DateTime localDate = DateTime.Now;

        print(localDate.ToString("yyyyMMddHHmmss"));

        foreach (var card in allCards)
        {
            string[] pathInstrument = audioclips[card.Id].ToString().Split(Path.DirectorySeparatorChar); 
            string[] nameInstrument = pathInstrument[pathInstrument.Length-1].Split('_');

            dataCollector.Add(new DataCollector(nameInstrument[0], card.numClicks, currentScene, nameInstrument[1], similarCards, sameMelody));
            
        }

        JSONSaving.SaveToJSON<DataCollector>(dataCollector, localDate.ToString("yyyyMMddHHmmss") + "_" + currentScene.ToString() + ".json");

    }

}