using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    [SerializeField] GameObject mouse;

    public bool hovered = false;
    public bool pressed = false;

    private UnityEngine.Color hoverColor = new UnityEngine.Color(1.0f, 1.0f, 0.0f, 1.0f);
    private UnityEngine.Color pressColor = new UnityEngine.Color(0.0f, 1.0f, 0.0f, 1.0f);
    private UnityEngine.Color originalColor; // Original color of the text in the button

    private TextMeshProUGUI buttonText;

    private void Start()
    {
        buttonText = this .GetComponentInChildren<TextMeshProUGUI>(); // Point to the text GUI
        originalColor = buttonText.color; // Save original color
    }
    
    private void OnTriggerStay2D(Collider2D targetObj)
    {
        if (targetObj.gameObject.tag == "selector")
        {
            hovered = true;
            buttonText.color = hoverColor;
        }
        if (targetObj.gameObject.tag == "selector" && mouse.GetComponent<MouseController>().xPressed == true)
        {
            StartCoroutine(ChangeColor()); // Allows to see the change of the selection color
        }

        if (targetObj.gameObject.tag == "selector" && mouse.GetComponent<MouseController>().xPressed == false)
        {
            pressed = false;
            
        }
    }

    private void OnTriggerExit2D(Collider2D targetObj)
    {
        hovered = false;
        pressed = false;

        buttonText.color = originalColor;
    }

    private IEnumerator ChangeColor()
    {
        buttonText.color = pressColor;
        yield return new WaitForSeconds(0.1f); // delay for 0.1 seconds
        pressed = true;

    }

}