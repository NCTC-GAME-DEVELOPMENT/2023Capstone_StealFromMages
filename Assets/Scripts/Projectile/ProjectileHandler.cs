using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileHandler : MonoBehaviourSingleton<ProjectileHandler> {
    public const int PROJECTILE_LIFETIME = 5;
    private List<GameObject> inactiveProjectiles;
    private List<GameObject> activeProjectiles;
    [SerializeField]
    private GameObject normalProjectile;
    void Awake() {
        base.Awake();
        inactiveProjectiles = new List<GameObject>();
        activeProjectiles = new List<GameObject>();
    }
    public void ShootProjectile(Vector3 _origin, Vector2 _dirAngle, NormalProjectileScriptableObject _stats, ProjectileTarget _targetTag = ProjectileTarget.All) {
        GameObject projectile;
        if (inactiveProjectiles.Count <= 0)
            projectile = GameObject.Instantiate(normalProjectile, transform);
        else {
            projectile = inactiveProjectiles[inactiveProjectiles.Count - 1];
            inactiveProjectiles.RemoveAt(inactiveProjectiles.Count - 1);
            projectile.SetActive(true);
        }
        projectile.transform.position = _origin;
        projectile.GetComponent<SpriteRenderer>().sprite = _stats.Sprite;
        projectile.AddComponent<NormalProjectile>().Initialize(_dirAngle, _stats, _targetTag, activeProjectiles.Count);
        activeProjectiles.Add(projectile);

    }
    public void RemoveProjectile(GameObject _object) {
        Destroy(_object.gameObject.GetComponent<IProjectile>());
        _object.gameObject.SetActive(false);
        activeProjectiles.Remove(_object);
        inactiveProjectiles.Add(_object);
    }
    public enum ProjectileTarget {
        All,
        Player,
        Enemy
    }
}
public abstract class IProjectile : MonoBehaviour {
}
