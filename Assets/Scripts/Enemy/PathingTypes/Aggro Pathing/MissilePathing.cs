using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
[Serializable]
public class MissilePathing : IAggroPathfindingType {
    public override Vector2 Pathfind(Vector2 _currentPosition) {
        return (Pathfinder.Instance.GetPlayerPosition() - _currentPosition).normalized;
    }
}
