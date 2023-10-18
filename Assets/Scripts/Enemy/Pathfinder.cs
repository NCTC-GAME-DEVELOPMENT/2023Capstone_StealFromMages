using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Pathfinder : MonoBehaviourSingleton<Pathfinder>
{
    private const int MOVE_DIAGONAL_COST = 14;
    private const int MOVE_STRAIGHT_COST = 10;
    [SerializeField]
    private readonly Transform playerTransform;
    public override void Awake() {
        base.Awake();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public struct PathNode {
        public int index;
        public int x, y;
        public int fCost, gCost, hCost;
        public bool isTraversable;
        public int originNodeIndex;
        private void CalculateFCost() {
            fCost = gCost + hCost;
        }
        private void CalculateDistanceCost(int2 _postionOne, int2 _positionTwo) {
            int xDistance = math.abs(_postionOne.x - _positionTwo.x);
            int yDistance = math.abs(_postionOne.y - _positionTwo.y);
            int remaining = math.abs(xDistance - yDistance);
            hCost = MOVE_DIAGONAL_COST * math.min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
        }
        public void SetNode(int _index, int2 _postionOne, int2 _positionTwo) {
            this.index = _index;
            gCost = int.MaxValue;
            CalculateDistanceCost(_postionOne, _positionTwo);
            CalculateFCost();
            originNodeIndex = -1;
        }
    }
}
