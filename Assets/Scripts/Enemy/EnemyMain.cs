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
    void Start() {
        IsAggro = false;
        IsPlayerInAggroDistance();
    }

    // Update is called once per frame
    void Update() {

    }
    public void IsPlayerInAggroDistance() {
        IsAggro = Vector2.Distance(Pathfinder.Instance.GetPlayerPosition(), transform.position) < AggroDistance;
        TickSystem.Instance?.CreateTimer(IsPlayerInAggroDistance, (uint)10);
    }
    void OnCollisionEnter2D(Collision2D _collision) {
        if (_collision.gameObject.tag == "Player") {
            _collision.gameObject.GetComponent<IHealth>()?.ApplyDamage(hitDamage);            
            return;
        }
    }
}
