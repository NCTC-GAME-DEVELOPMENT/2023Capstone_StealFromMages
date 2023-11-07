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
    private float aggroDistance;
    [SerializeField]
    private bool isShooter;
    [SerializeField]
    private NormalProjectileScriptableObject projectileScriptableObject;
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

        // Need To Seperate isAggro into Main Mono that other Scripts can call
        if (isAggro && isShooter && projectileScriptableObject is not null) {
            ProjectileHandler.Instance.ShootProjectile(
                GetPosition(),
                (Pathfinder.Instance.GetPlayerPosition() - (Vector2)GetPosition()).normalized,
                projectileScriptableObject,
                ProjectileHandler.ProjectileTarget.Player
            );
            isShooter = false;
        }

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
[Serializable]
public abstract class IPassivePathfindingType : MonoBehaviour {
    // Make Sure That you Return A Normalized Vector
    public abstract Vector2 Pathfind(Vector2 _currentPosition);
}
public abstract class IAggroPathfindingType : MonoBehaviour {
    // Make Sure That you Return A Normalized Vector
    public abstract Vector2 Pathfind(Vector2 _currentPosition);
}

