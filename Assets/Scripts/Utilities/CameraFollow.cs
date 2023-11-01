using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public void Update() {
        Vector3 TargetPosition = Pathfinder.Instance.GetPlayerPosition();
        transform.position = new Vector3(TargetPosition.x, TargetPosition.y, -10);
    }
}
