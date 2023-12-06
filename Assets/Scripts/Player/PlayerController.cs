using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Animator public var
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    public int playerNumber = 1;
    public float moveSpeed = 4f;
    public float triggerActAt = .9f;
    [SerializeField]
    private Collider2D collisionCollider;
    [SerializeField]
    private Collider2D HitBoxCollider;
    InputData input; 
    Rigidbody2D rb;

    //audio
    public AudioClip footSteps;
    public AudioSource audioSource;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        Physics2D.IgnoreCollision(HitBoxCollider, collisionCollider, true);

        animator = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        //assign audio source
        //audioSource = GetComponent<AudioSource>();
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
        if (rb.velocity.x < 0)
        {
            //moving right
            spriteRenderer.flipX = false;

        }
        else
        {
            //moving left
            spriteRenderer.flipX = true;

        }
        if (rb.velocity.magnitude > 0)
        {
            //audioSource.PlayOneShot(footSteps);
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
