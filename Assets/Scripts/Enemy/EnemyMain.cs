using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMain : MonoBehaviour {
    [SerializeField]
    public float AggroDistance = 2;
    [Tooltip("In Ticks (20 per second)")]
    [SerializeField]
    private uint hitCooldown;
    [SerializeField]
    private float hitDamage;
    [SerializeField]
    public bool IsAggro;
    [SerializeField]
    private bool RecentHit;
    private bool HasDied;
    public event Action OnDeathEvent;
    void Start() {
        IsAggro = false;
        IsPlayerInAggroDistance();
    }

    // Update is called once per frame
    void Update() {

    }
    public void OnDeath() {
        HasDied = true;
        TickSystem.Instance?.CreateTimer(DestroyEnemy, (uint)11);
        OnDeathEvent?.Invoke();
    }
    public void SwitchHit() {
        RecentHit = false;
    }
    public void IsPlayerInAggroDistance() {
        if (!HasDied) {
            IsAggro = Vector2.Distance(Pathfinder.Instance.GetPlayerPosition(), transform.position) < AggroDistance;
            TickSystem.Instance?.CreateTimer(IsPlayerInAggroDistance, (uint)10);
        }
    }
    void OnCollisionEnter2D(Collision2D _collision) {
        if (!RecentHit) {
            if (_collision.gameObject.tag == "Player") {
                _collision.gameObject.GetComponent<IHealth>()?.ApplyDamage(hitDamage);
                TickSystem.Instance?.CreateTimer(SwitchHit, (uint)5);
                return;
            }
        }
    }
    private void DestroyEnemy() {
        gameObject.SetActive(false);
    }
}
