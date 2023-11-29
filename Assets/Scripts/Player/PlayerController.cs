using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Animator public var (drag animator already attached to player to this field in editor)
    public Animator animator;

    public int playerNumber = 1;
    public float moveSpeed = 4f;
    public float triggerActAt = .9f;
    [SerializeField]
    private Collider2D collisionCollider;
    [SerializeField]
    private Collider2D HitBoxCollider;
    InputData input; 
    Rigidbody2D rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        Physics2D.IgnoreCollision(HitBoxCollider, collisionCollider, true);
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
        //RotatePlayer(input.rightStick); 

        if (input.rightTrigger > triggerActAt)
        {
            //Debug.Log("Fire"); 
        }

        //if player is moving (speed greater than 0.1), play movement anim
        animator.SetFloat("Speed", rb.velocity.magnitude);

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
