using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
[Serializable]
public class RigidPatrolPathing : IPassivePathfindingType {
    /* Todo:
     * Detection Radius 
     */
    [SerializeField]
    private List<PatrolPoint> patrolPoints;
    [SerializeField]
    private PatrolType type;
    [SerializeField]
    private bool IsGoingForward;
    private int index;
    private bool isWaiting;
    private float timeWaited;
    public override Vector2 Pathfind(Vector2 _currentPosition) {
        if (isWaiting) {
            if (timeWaited < patrolPoints[index].WaitTime) {
                timeWaited += Time.deltaTime;
                return new Vector2(0, 0);
            }
            else {
                timeWaited = 0;
                ProceedWithRoute();
            }
        }
        else {
            if (IsPointReached(patrolPoints[index], _currentPosition))
                ProceedWithRoute();
        }
        return GetHeading(_currentPosition);
    }
    static protected bool IsPointReached(Vector2 _posOne, Vector2 _posTwo) {
        return Vector2.Distance(_posOne, _posTwo) < .1f;
    }
    private void ProceedWithRoute() {
        if (!isWaiting && patrolPoints[index].WaitTime > 0)
            isWaiting = true;
        else {
            isWaiting = false;
            switch (type) {
                case PatrolType.Cycle:
                    index += IsGoingForward ? 1 : -1;
                    if (IsGoingForward && index >= patrolPoints.Count)
                        index = 0;
                    else
                        if (!IsGoingForward && index < 0)
                            index = patrolPoints.Count - 1;
                    break;
                case PatrolType.BackAndForth:
                    index += IsGoingForward ? 1 : -1;
                    if (IsGoingForward && index >= patrolPoints.Count) {
                        IsGoingForward = false;
                        index -= 2;
                    }
                    else
                        if (!IsGoingForward && index < 0) {
                        IsGoingForward = true;
                        index++;
                    }

                    break;
            }
        }
    } 
    private Vector2 GetHeading(Vector2 _currentPosition) {
        return (patrolPoints[index] - _currentPosition).normalized;
    }
    public enum PatrolType {
        Cycle,
        BackAndForth
    }
    [System.Serializable]
    public struct PatrolPoint {
        [SerializeField]
        public Vector2 Coordinates;
        [Tooltip("Time to wait After Reaching Point in Seconds")]
        // Time to wait After Reaching Point in Seconds
        public float WaitTime;
        public static implicit operator Vector2(PatrolPoint _point) => _point.Coordinates;
    }
}

