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
    void Start() {       
        passivePathfinding = GetComponent<IPassivePathfindingType>();
        aggroPathfinding = GetComponent<IAggroPathfindingType>();
        enemyMain = GetComponent<EnemyMain>();
    }

    // Update is called once per frame
    void Update() {
        if (movementSpeed != 0)
            if (aggroPathfinding is not null)
                Move(enemyMain.IsAggro ? aggroPathfinding.Pathfind(transform.position) : passivePathfinding.Pathfind(transform.position));
    }
    public Vector3 GetPosition() => transform.position;
    // Make Sure That the Parmemeter is Normalized First
    public void IsPlayerInAggroDistance() {
        enemyMain.IsAggro = Vector2.Distance(Pathfinder.Instance.GetPlayerPosition(), transform.position) < enemyMain.AggroDistance;
        TickSystem.Instance?.CreateTimer(IsPlayerInAggroDistance, (uint)10);
    }
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

