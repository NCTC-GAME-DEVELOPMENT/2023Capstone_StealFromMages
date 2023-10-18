using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    new private Rigidbody2D rigidbody;
    
    private IPathfindingType pathfinding;
    
    void Start() {
        pathfinding = GetComponent<IPathfindingType>();
    }

    // Update is called once per frame
    void Update() {

        if (movementSpeed != 0) {
            Move(pathfinding.Pathfind(transform.position));
        }
    }
    // Make Sure That the Parmemeter is Normalized First
    private void Move(Vector2 _desiredDirection) {
            rigidbody.velocity = _desiredDirection * movementSpeed;
    }

}
public interface IPathfindingType {
    // Make Sure That you Return A Normalized Vector
    public abstract Vector2 Pathfind(Vector2 _currentPosition);
}
