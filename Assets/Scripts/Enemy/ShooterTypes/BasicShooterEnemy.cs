using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicShooterEnemy : MonoBehaviour
{
    [SerializeField]
    private EnemyMain enemyMain;
    [SerializeField]
    private NormalProjectileScriptableObject projectileScriptableObject;
    [SerializeField]
    private bool canFire;
    [Tooltip("In Seconds")]
    [SerializeField]
    private int reloadTime = 5;
    private void Start() {
        enemyMain = GetComponent<EnemyMain>();
        canFire = true;
}
    private void Update() {
        if (enemyMain.IsAggro && canFire) {
            ProjectileHandler.Instance.ShootProjectile(
                transform.position,
                (Pathfinder.Instance.GetPlayerPosition() - (Vector2)transform.position).normalized,
                projectileScriptableObject,
                ProjectileHandler.ProjectileTarget.Player
            );
            canFire = false;
            TickSystem.Instance.CreateTimer(CanFire, reloadTime);
        }
    }
    private void CanFire() => canFire = true;
}
