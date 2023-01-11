﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCard : MonoBehaviour {
	[SerializeField] GameObject cardBack;
	[SerializeField] GameObject imHovered;
	[SerializeField] GameObject mouse; 
	[SerializeField] SceneController controller;

	private bool imPressed = false;
	private bool imTouched = false;

	private string soundInstrument;

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

    public void SetCard(int id, Sprite image) {
		_id = id;
        GetComponent<SpriteRenderer>().sprite = image;
    }

    public void SetAudio(int id, string[] listOfEvents)
    {
		_id = id;
		soundInstrument = "event:/Instruments/" + listOfEvents[id];

	}

    private void OnMouseDown()
    {
        controller.CardRevealed(this);

		FMODUnity.RuntimeManager.PlayOneShot(soundInstrument);
    }

	// Controller PS5 interaction
    private void Pressed( )
    {	
		controller.CardRevealed(this);

		FMODUnity.RuntimeManager.PlayOneShot(soundInstrument);

		imTouched = false;
    }

	public void Unreveal() {
		cardBack.SetActive(true);
	}

	public void Reveal()
	{
		cardBack.SetActive(false);
	}

	private void OnCollisionEnter2D(Collision2D collision)
    {
		
		imTouched = true;
		//Debug.Log(imTouched);
	}

 
    private void OnCollisionExit2D(Collision2D collision)
    {
		imHovered.SetActive(false);
		imTouched = false;
		//Debug.Log(imTouched);
	}


}
