using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioStream;

public class MemoryCard : MonoBehaviour {
	[SerializeField] GameObject cardBack;
	[SerializeField] GameObject imHovered;
    [SerializeField] GameObject iAmPressed;
    [SerializeField] GameObject mouse; 
	[SerializeField] SceneController controller;

	private bool imTouched = false;
	public bool alreadyMatched = false;

	private List<FMOD_SystemW.OUTPUT_DEVICE> availableOutputs = new List<FMOD_SystemW.OUTPUT_DEVICE>();
	public AudioSourceOutputDevice outputDevice;
	
	// Extra audio source for additional audio device
	public AudioSource computerAudio;

	private float prevSoundLevel = 0f;
	private float prevHapticsLevel = 0f;
	public int numClicks = 0;


    private int _id;
	public int Id {
		get {return _id;}
	}


	private void Start()
	{
		prevSoundLevel = PlayerPrefs.GetFloat("soundLevel");
		prevHapticsLevel = PlayerPrefs.GetFloat("hapticLevel");

		StartCoroutine(SetOutputChannel(availableOutputs, 0, 0, 1, 2, 3, outputDevice, prevHapticsLevel));
		computerAudio.volume = prevSoundLevel;

	}

	private void Update()
    {
		if (imTouched == true && mouse.GetComponent<mouseController>().xPressed == true)
        {
			Pressed();
		}


		// Update sound and haptic levels when changed in menu
		if (prevSoundLevel != PlayerPrefs.GetFloat("soundLevel"))
		{
            computerAudio.volume = PlayerPrefs.GetFloat("soundLevel");
			prevSoundLevel = PlayerPrefs.GetFloat("soundLevel");
        }

		if (prevHapticsLevel != PlayerPrefs.GetFloat("hapticLevel"))
		{
            StartCoroutine(SetOutputChannel(availableOutputs, 0, 0, 1, 2, 3, outputDevice, PlayerPrefs.GetFloat("hapticLevel")));
			prevHapticsLevel = PlayerPrefs.GetFloat("hapticLevel");
        }


    }

	IEnumerator SetOutputChannel(List<FMOD_SystemW.OUTPUT_DEVICE> availableOutputs, int selectedOutput, int selectedOutputChannel1, int selectedOutputChannel2, int selectedOutputChannel3, int selectedOutputChannel4, AudioSourceOutputDevice audioSourceOutput, float outputLevel)
	{
		while (!audioSourceOutput.ready)
			yield return null;

		this.availableOutputs = FMOD_SystemW.AvailableOutputs(audioSourceOutput.logLevel, audioSourceOutput.gameObject.name, audioSourceOutput.OnError);

		// in case of MONO input we have matrix with outpuchannels rows and 1 column:
		var outchannels = 4;
		var inchannels = 1; // MONO source


		var mixMatrix = new float[outchannels * inchannels];
		System.Array.Clear(mixMatrix, 0, mixMatrix.Length);

		// we'll set level just on requested output channel:
		mixMatrix[selectedOutputChannel1] = outputLevel;
		mixMatrix[selectedOutputChannel2] = outputLevel;
		mixMatrix[selectedOutputChannel3] = outputLevel;
		mixMatrix[selectedOutputChannel4] = outputLevel;


		audioSourceOutput.SetUnitySound_MixMatrix(mixMatrix, outchannels, inchannels);
	}


	public void SetCard(int id, Sprite image) {
		_id = id;
		GetComponent<SpriteRenderer>().sprite = image;
		
	}

	public void SetAudio(int id, AudioClip lyd)
    {
		_id = id;
		GetComponent<AudioSource>().clip = lyd;
		computerAudio.clip = lyd;
    }

    private void OnMouseDown()
    {

        if (!PauseMenuController.GameIsPaused || !alreadyMatched)
		{
            controller.CardRevealed(this);

            GetComponent<AudioSource>().Play();
            computerAudio.Play();
        }

    }


	private void Pressed()
	{

		if (!PauseMenuController.GameIsPaused || !alreadyMatched)
		{
			controller.CardRevealed(this);
			iAmPressed.SetActive(true);

			if(!alreadyMatched)
			{
                GetComponent<AudioSource>().Play();
                computerAudio.Play();
            }	

            numClicks++;
            
            imTouched = false;
		}

    }

	public void Unreveal() {

        if (!alreadyMatched)
		{
            cardBack.SetActive(true);
            if (iAmPressed.activeSelf)
            {
                iAmPressed.SetActive(false);

            }
        }


    }

	public void Matched()
	{
        SpriteRenderer sprite = iAmPressed.GetComponent<SpriteRenderer>();
		sprite.color = Color.green;

	}

	public void Reveal()
	{
		cardBack.SetActive(false);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!PauseMenuController.GameIsPaused || !alreadyMatched)
        {
            imHovered.SetActive(true);
            imTouched = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!PauseMenuController.GameIsPaused)
        {
            imHovered.SetActive(false);
            imTouched = false;

            GetComponent<AudioSource>().Stop();
            computerAudio.Stop();
        }
    }
}
