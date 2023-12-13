using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerBossPetalShooter : MonoBehaviour
{
    [SerializeField]
    NormalProjectileScriptableObject attackStats;

    //audio
    public AudioClip shootAttack;
    private AudioSource audioSource;

    public void Start()
    {
        //assign audio source
        audioSource = GetComponent<AudioSource>();
    }
    public void Shoot() {
        ProjectileHandler.Instance.ShootProjectile(transform.position, (Pathfinder.Instance.GetPlayerPosition() - (Vector2)transform.position).normalized, attackStats, ProjectileHandler.ProjectileTarget.Player);
        audioSource.PlayOneShot(shootAttack);
    }
}
