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
        passivePathfinding = GetComponent<IPassivePathfindingType>();
        aggroPathfinding = GetComponent <IAggroPathfindingType>();
    }

    // Update is called once per frame
    void Update() {

        if (movementSpeed != 0)
            Move(isAggro ? aggroPathfinding == null ? aggroPathfinding.Pathfind(transform.position) : passivePathfinding.Pathfind(transform.position) : passivePathfinding.Pathfind(transform.position));
        
    }
    public Vector3 GetPosition() => transform.position; 
    // Make Sure That the Parmemeter is Normalized First
    private void Move(Vector2 _desiredDirection) {
            rigidbody.velocity = _desiredDirection * movementSpeed;
    }
    public void SwitchAggroStance()
    {
        isAggro = true;
    }
    public void SwitchPassiveStance()
    {
        isAggro = false;
    }

}
public interface IPassivePathfindingType {
    // Make Sure That you Return A Normalized Vector
    public abstract Vector2 Pathfind(Vector2 _currentPosition);
}public interface IAggroPathfindingType {
    // Make Sure That you Return A Normalized Vector
    public abstract Vector2 Pathfind(Vector2 _currentPosition);
}

