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


	private int _id;
	public int Id {
		get {return _id;}
	}

    private void Update()
    {
		if (imTouched == true && mouse.GetComponent<mouseController>().xPressed == true)
        {
			pressed();
		}



	}

    public void SetCard(int id, Sprite image) {
		_id = id;
		GetComponent<SpriteRenderer>().sprite = image;
		
	}

	public void setAudio(int id, AudioClip lyd)
    {
		_id = id;
		GetComponent<AudioSource>().clip = lyd; 
    }

    private void OnMouseDown()
    {
		cardBack.SetActive(false);
		controller.CardRevealed(this);
		GetComponent<AudioSource>().Play();
	}


    private void pressed()
    {
		cardBack.SetActive(false);
		controller.CardRevealed(this);
		GetComponent<AudioSource>().Play();
		imTouched = false;
    }

	public void Unreveal() {
		cardBack.SetActive(true);
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
	}


}
