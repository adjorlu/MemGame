using System.Collections;
using System.Collections.Generic;
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


    void OnCollisionStay2D(Collision2D targetObj)
    {
        if (targetObj.gameObject.tag == "selector")
        {
            hovered= true;
        }

        if (targetObj.gameObject.tag == "selector" && mouse.GetComponent<mouseController>().xPressed == true)
        {
            pressed= true;
        }

        if (targetObj.gameObject.tag == "selector" && mouse.GetComponent<mouseController>().xPressed == false)
        {
            pressed = false;
        }
    }

    private void OnCollisionExit2D(Collision2D targetObj)
    {
        hovered = false;
        pressed = false;
    }
}