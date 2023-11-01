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
    private float aggroDistance;
    public float AggroDistance {
        get => aggroDistance;
    }
    private bool isAggro = false;
    void Start() {
        IsPlayerInAggroDistance();
        passivePathfinding = GetComponent<IPassivePathfindingType>();
        aggroPathfinding = GetComponent<IAggroPathfindingType>();
    }

    // Update is called once per frame
    void Update() {
        if (movementSpeed != 0)
            if (aggroPathfinding is not null)
                Move(isAggro ? aggroPathfinding.Pathfind(transform.position) : passivePathfinding.Pathfind(transform.position));

    }
    public Vector3 GetPosition() => transform.position;
    // Make Sure That the Parmemeter is Normalized First
    public void IsPlayerInAggroDistance() {
        isAggro = Vector2.Distance(Pathfinder.Instance.GetPlayerPosition(), transform.position) < AggroDistance;
        TickSystem.Instance?.CreateTimer(IsPlayerInAggroDistance, (uint)10);
    }
    private void Move(Vector2 _desiredDirection) {
        rigidbody.velocity = _desiredDirection * movementSpeed;
    }
}
public interface IPassivePathfindingType {
    // Make Sure That you Return A Normalized Vector
    public abstract Vector2 Pathfind(Vector2 _currentPosition);
}
public interface IAggroPathfindingType {
    // Make Sure That you Return A Normalized Vector
    public abstract Vector2 Pathfind(Vector2 _currentPosition);
}

