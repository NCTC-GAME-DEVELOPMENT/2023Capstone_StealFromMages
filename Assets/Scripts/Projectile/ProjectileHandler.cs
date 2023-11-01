using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHandler : MonoBehaviourSingleton<ProjectileHandler> {
    private List<ProjectileBase> inactiveProjectiles;
    private List<ProjectileBase> activeProjectiles;
    void Awake() {
        base.Awake();
    }
}
