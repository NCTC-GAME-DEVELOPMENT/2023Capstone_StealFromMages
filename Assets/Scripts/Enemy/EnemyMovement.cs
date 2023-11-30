using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    new private Rigidbody2D rigidbody;
    [SerializeField]
    private IPassivePathfindingType passivePathfinding;
    [SerializeField]
    private IAggroPathfindingType aggroPathfinding;
    [SerializeField]
    private EnemyMain enemyMain;

    public Animator animator;
    public SpriteRenderer spriteRenderer;


    void Start() {       
        passivePathfinding = GetComponent<IPassivePathfindingType>();
        aggroPathfinding = GetComponent<IAggroPathfindingType>();
        enemyMain = GetComponent<EnemyMain>();

        animator = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        if (movementSpeed != 0)
            if (aggroPathfinding is not null)
                Move(enemyMain.IsAggro ? aggroPathfinding.Pathfind(transform.position) : passivePathfinding.Pathfind(transform.position));


        //if player is moving (speed greater than 0.1), play movement anim
        animator.SetFloat("Speed", rigidbody.velocity.magnitude);
        if (rigidbody.velocity.x < 0)
        {
            //moving right
            spriteRenderer.flipX = false;
            //WIP
            //isFacingLeft = false;
        }
        else
        {
            //moving left
            spriteRenderer.flipX = true;
            //WIP
            //isFacingLeft = true;
        }


    }
    public Vector3 GetPosition() => transform.position;
    // Make Sure That the Parmemeter is Normalized First
    private void Move(Vector2 _desiredDirection) {
        rigidbody.velocity = _desiredDirection * movementSpeed;
    }
}
[Serializable]
public abstract class IPassivePathfindingType : MonoBehaviour {
    // Make Sure That you Return A Normalized Vector
    public abstract Vector2 Pathfind(Vector2 _currentPosition);
}
public abstract class IAggroPathfindingType : MonoBehaviour {
    // Make Sure That you Return A Normalized Vector
    public abstract Vector2 Pathfind(Vector2 _currentPosition);
}

