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

	private List<FMOD_SystemW.OUTPUT_DEVICE> availableOutputs = new List<FMOD_SystemW.OUTPUT_DEVICE>();
	public AudioSourceOutputDevice outputDevice;

	public AudioSource computerAudio;

	private int _id;
	public int Id {
		get {return _id;}
	}

    private void Update()
    {
		if (imTouched == true && mouse.GetComponent<mouseController>().xPressed == true)
        {
			Pressed();
		}



	}

	IEnumerator SetOutputChannel(List<FMOD_SystemW.OUTPUT_DEVICE> availableOutputs, int selectedOutput, int selectedOutputChannel1, int selectedOutputChannel2, int selectedOutputChannel3, int selectedOutputChannel4, AudioSourceOutputDevice audioSourceOutput, float outputLevel)
	{
		while(!audioSourceOutput.ready)
			yield return null;

		print(availableOutputs.Count);

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


    private void Start()
    {
		StartCoroutine(SetOutputChannel(availableOutputs, 0, 0, 1, 2, 3, outputDevice, 1.0f));
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
        controller.CardRevealed(this);
        GetComponent<AudioSource>().Play();
		computerAudio.Play();
	}


    private void Pressed()
    {	
		controller.CardRevealed(this);
        iAmPressed.SetActive(true);
        // Do we need this?
        GetComponent<AudioSource>().Play();
		computerAudio.Play();

		imTouched = false;
    }

	public void Unreveal() {
		cardBack.SetActive(true);
        if (iAmPressed.activeSelf)
        {
            iAmPressed.SetActive(false);
         
        }
        //iAmPressed.SetActive(false);
    }

	public void Reveal()
	{
		cardBack.SetActive(false);
	}

	private void OnCollisionEnter2D(Collision2D collision)
    {
		imHovered.SetActive(true);
		imTouched = true;
		//Debug.Log(imTouched);

	}

 
    private void OnCollisionExit2D(Collision2D collision)
    {
		imHovered.SetActive(false);
		imTouched = false;
		//Debug.Log(imTouched);
		GetComponent<AudioSource>().Stop();
		computerAudio.Stop();
	}




}
