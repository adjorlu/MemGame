using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class MouseController : MonoBehaviour
{


    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private float speed;
    public bool xPressed = false; 
    private Vector2 moveInputValue; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    private void OnMove(InputValue value)
    {
        moveInputValue = value.Get<Vector2>();
        //Debug.Log(moveInputValue);
    }

    private void OnButtonRegular()
    {
        Debug.Log("I AM X BUTTON");
        xPressed = true; 
    }

    private void OnButtonReleased()
    {
        Debug.Log("I AM NO LONGER PRESSED");
        xPressed = false;
    }
    private void moveLogic()
    {
        Vector2 result = moveInputValue * speed * Time.fixedDeltaTime;
        rb2D.velocity = result;
    }

    private void FixedUpdate()
    {
        moveLogic();
    }
}
