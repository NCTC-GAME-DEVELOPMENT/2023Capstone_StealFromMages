using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int playerNumber = 1;
    public float moveSpeed = 4f;
    public float triggerActAt = .9f; 
    
    InputData input; 
    Rigidbody2D rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        input = InputPoller.reference.GetInput(playerNumber);
        rb.velocity = Vector2.zero;

        if (playerNumber == 0)
        {
            KBMConversions(); 
        }

        MovePlayer(input.leftStick);
        RotatePlayer(input.rightStick); 

        if (input.rightTrigger > triggerActAt)
        {
            Debug.Log("Fire"); 
        }    
        
    }

    void KBMConversions()
    {
        //input.rightStick = (input.rightStick - gameObject.transform.position).normalized;
    }

    void MovePlayer (Vector2 value)
    {
        rb.velocity = value * moveSpeed;
    }

    void RotatePlayer(Vector2 value)
    {
        gameObject.transform.right = value; 
    }
}
