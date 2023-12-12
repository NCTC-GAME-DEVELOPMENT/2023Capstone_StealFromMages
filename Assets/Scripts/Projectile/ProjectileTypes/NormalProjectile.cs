using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class NormalProjectile : IProjectile {
    private int projectileLifetime = ProjectileHandler.PROJECTILE_LIFETIME;
    private float projectileSpeed;
    private float projectileDamage;
    private Vector2 projectileDirAngle;
    private new Rigidbody2D rigidbody;
    private string[] targetTags;
    private bool hasHit;
    private int projectileIndex;
    public bool IsActive = false;
    void Awake() {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }
    void Update() {
        if (IsActive) {
            rigidbody.velocity = projectileDirAngle * projectileSpeed;
        }
    }
    public void Initialize(Vector2 _dirAngle, NormalProjectileScriptableObject _stats, ProjectileHandler.ProjectileTarget _targetTag, int _index) {
        targetTags = null;
        projectileDirAngle = _dirAngle;
        projectileSpeed = _stats.Speed;
        projectileDamage = _stats.Damage;
        projectileIndex = _index;
        hasHit = false;
        switch (_targetTag) {
            case ProjectileHandler.ProjectileTarget.Enemy:
                targetTags = new string[] { "Enemy", "Terrain" };
                break;
            case ProjectileHandler.ProjectileTarget.Player:
                targetTags = new string[] { "Player", "Terrain" };
                break;
            case ProjectileHandler.ProjectileTarget.All:
                targetTags = new string[] { "Enemy", "Player", "Terrain" };
                break;
        }
        TickSystem.Instance.CreateTimer(Disable, (int)projectileLifetime);
        IsActive = true;
    }
    public void Initialize(Vector2 _dirAngle, WeaponScriptableObject _stats, ProjectileHandler.ProjectileTarget _targetTag, int _index) {
        targetTags = null;
        projectileDirAngle = _dirAngle;
        projectileSpeed = _stats.Speed;
        projectileDamage = _stats.Damage;
        projectileIndex = _index;
        hasHit = false;
        switch (_targetTag) {
            case ProjectileHandler.ProjectileTarget.Enemy:
                targetTags = new string[] { "Enemy", "Terrain" };
                break;
            case ProjectileHandler.ProjectileTarget.Player:
                targetTags = new string[] { "Player", "Terrain" };
                break;
            case ProjectileHandler.ProjectileTarget.All:
                targetTags = new string[] { "Enemy", "Player", "Terrain" };
                break;
        }
        TickSystem.Instance.CreateTimer(Disable, (int)projectileLifetime);
        IsActive = true;
    }
    private void OnTriggerEnter2D(Collider2D _collision) {
        if (IsActive) {
            if (_collision.gameObject.tag == "Terrain") {
                Disable();
                return;
            }
            foreach (var tag in targetTags) {
                if (_collision.gameObject.tag == tag) {
                    _collision.gameObject.GetComponent<IHealth>()?.ApplyDamage(projectileDamage);
                    hasHit = true;
                }
            }
            if(hasHit) {
                    Disable();
                    return;
            }
        }
    }
    public void Disable() {
        if (IsActive) {
            IsActive = !IsActive;
            targetTags = null;
            ProjectileHandler.Instance.RemoveProjectile(this.gameObject);
        }
    }
}
