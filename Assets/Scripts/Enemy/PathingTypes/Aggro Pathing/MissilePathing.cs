using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissilePathing : MonoBehaviour, IAggroPathfindingType {
    public Vector2 Pathfind(Vector2 _currentPosition) {
        return (Pathfinder.Instance.GetPlayerPosition() - _currentPosition).normalized;
    }
}
