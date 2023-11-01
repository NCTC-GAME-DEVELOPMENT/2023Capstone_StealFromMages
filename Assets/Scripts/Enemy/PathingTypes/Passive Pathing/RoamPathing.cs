
using Unity.VisualScripting;
using UnityEngine;

public class RoamPathing : MonoBehaviour, IPassivePathfindingType {
    /* Todo:
     * Allow for Random Wait Times, and Minimum distances
     */
    [SerializeField]
    private uint roamRadius = 10;
    [SerializeField]
    private float minimumDistance = .5f;
    [SerializeField]
    [Tooltip("WIP")]
    private float minimumDistanceFromOrigin = 0;
    private Vector2 targetPosition;
    private Vector2 originPosition;
    private bool isWaiting;
    private float timeWaited;
    [SerializeField]
    private float timeToWait = 5;
    void Awake() {
        originPosition = transform.position;
        FindNewTarget(originPosition);
    }
    public Vector2 Pathfind(Vector2 _currentPosition) {
        if (isWaiting) {
            if (timeWaited <= timeToWait) {
                timeWaited += Time.deltaTime;
                return Vector2.zero;
            }
            else {
                isWaiting = false;
                timeWaited = 0;
            }

        } 
        else
            if (IsPointReached(targetPosition, _currentPosition)) {
                isWaiting = true;
                FindNewTarget(_currentPosition);
        }
        return GetHeading(_currentPosition);
    }
    private Vector2 GetHeading(Vector2 _currentPosition) {
        return (targetPosition - _currentPosition).normalized;
    }
    // This is Very Clunky
    private void FindNewTarget(Vector2 _currentPosition) {
        targetPosition = originPosition + (new Vector2(Random.Range(minimumDistance, roamRadius), Random.Range(minimumDistance, roamRadius)) * (Random.Range(0, 2) * 2 - 1));
        Debug.Log("Roaming Target Position: " + targetPosition);
    }
    static protected bool IsPointReached(Vector2 _posOne, Vector2 _posTwo) {
        return Vector2.Distance(_posOne, _posTwo) < .1f;
    }
}
