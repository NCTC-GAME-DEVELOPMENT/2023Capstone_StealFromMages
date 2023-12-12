using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerBossPetalShooter : MonoBehaviour
{
    [SerializeField]
    NormalProjectileScriptableObject attackStats;
    public void Shoot() {
        ProjectileHandler.Instance.ShootProjectile(transform.position, (Pathfinder.Instance.GetPlayerPosition() - (Vector2)transform.position).normalized, attackStats, ProjectileHandler.ProjectileTarget.Player);
    }
}
