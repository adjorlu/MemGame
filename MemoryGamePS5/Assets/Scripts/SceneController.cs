using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using AudioStream;



public class SceneController : MonoBehaviour {
	public int gridRows = 2;
	public int gridCols = 4;
	public float offsetX = 2f;
	public float offsetY = 2.5f;
	public int[] numbers = {0, 0, 1, 1, 2, 2, 3, 3};

	[SerializeField] MemoryCard originalCard;
	[SerializeField] Sprite[] images;
	[SerializeField] AudioClip[] sounds; 
	[SerializeField] TMP_Text scoreLabel;
	
	private MemoryCard firstRevealed;
	private MemoryCard secondRevealed;
	private int score = 0;
	private int currentScene;
	private List<int> matchId = new List<int>();


	//private List<FMOD_SystemW.OUTPUT_DEVICE> availableOutputs = new List<FMOD_SystemW.OUTPUT_DEVICE>();
	//public AudioSourceOutputDevice outputDevice;

	public bool canReveal {
		get {return secondRevealed == null;}
	}

	// Use this for initialization
	void Start() {
		currentScene = SceneManager.GetActiveScene().buildIndex;

		Debug.Log("Scene number is: " + currentScene);
		Vector3 startPos = originalCard.transform.position;

		// create shuffled list of cards
		numbers = ShuffleArray(numbers);

		// place cards in a grid
		for (int i = 0; i < gridCols; i++) {
			for (int j = 0; j < gridRows; j++) {
				MemoryCard card;

				// use the original for the first grid space
				if (i == 0 && j == 0) {
					card = originalCard;
				} else {
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

		//SetOutputChannel(availableOutputs, 0, 0, 1, 2, 3, outputDevice, 1.0f);


		//if (Application.isPlaying)
		//{
		//	AudioSourceOutputDeviceDemo.UpdateOutputDevicesList();
		//}
	}

	// everyday im Knuth shuffling 
	private int[] ShuffleArray(int[] numbers) {
		int[] newArray = numbers.Clone() as int[];
		for (int i = 0; i < newArray.Length; i++ ) {
			int tmp = newArray[i];
			int r = Random.Range(i, newArray.Length);
			newArray[i] = newArray[r];
			newArray[r] = tmp;
		}
		return newArray;
	}

	public void CardRevealed(MemoryCard card) {
		if (firstRevealed == null) {
			firstRevealed = card;
		} else {
			secondRevealed = card;
			StartCoroutine(CheckMatch());
		}
	}
	
	private IEnumerator CheckMatch() {

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
			scoreLabel.text = $"Score: {score}";

			// show the instrument 
			firstRevealed.Reveal();
			secondRevealed.Reveal();




			if (score == 4)
            {
				yield return new WaitForSeconds(2.0f);
				ChangeScene();
			}
		}

		// otherwise turn them back over after .5s pause
		else {
			yield return new WaitForSeconds(.5f);

			if (!matchId.Contains(firstRevealed.Id))
            {
				firstRevealed.Unreveal();
				secondRevealed.Unreveal();
			}
		}
		
		firstRevealed = null;
		secondRevealed = null;
	}

	public void Restart() {
		SceneManager.LoadScene("Scene");
	}

    private void ChangeScene()
    {
		SceneManager.LoadScene(currentScene + 1);
    }

	//private void SetOutputChannel(List<FMOD_SystemW.OUTPUT_DEVICE> availableOutputs, int selectedOutput, int selectedOutputChannel1, int selectedOutputChannel2, int selectedOutputChannel3, int selectedOutputChannel4, AudioSourceOutputDevice audioSourceOutput, float outputLevel)
	//{
	//	// not y ready
	//	if (availableOutputs.Count <= selectedOutput)
	//		return;

	//	// in case of MONO input we have matrix with outpuchannels rows and 1 column:
	//	var outchannels = availableOutputs[selectedOutput].channels;
	//	var inchannels = 1; // MONO source

	//	var mixMatrix = new float[outchannels * inchannels];
	//	System.Array.Clear(mixMatrix, 0, mixMatrix.Length);

	//	// we'll set level just on requested output channel:
	//	mixMatrix[selectedOutputChannel1] = outputLevel;
	//	mixMatrix[selectedOutputChannel2] = outputLevel;
	//	mixMatrix[selectedOutputChannel3] = outputLevel;
	//	mixMatrix[selectedOutputChannel4] = outputLevel;


	//	audioSourceOutput.SetUnitySound_MixMatrix(mixMatrix, outchannels, inchannels);
	//}
}
