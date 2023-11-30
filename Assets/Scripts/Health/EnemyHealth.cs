using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IHealth {
    [SerializeField]
    private float health;    
    public event Action<GameObject> OnDeathCallback;
    [SerializeField]
    private EnemyMain enemyMain;

    //Animator public var (drag animator already attached to player to this field in editor)
    public Animator animator;

    void Update()
    {
        //if enemy is dead, play death anim
        animator.SetFloat("Health", health);
    }
    void Start () {
    enemyMain = GetComponent<EnemyMain>();
    }
    public float GetHealth() => health;
    public void ApplyDamage(float _damage) {
        Debug.Log(_damage + " Damage");
        health -= _damage;
        if (health < 0) {
            OnDeathCallback?.Invoke(gameObject);
            Debug.Log("Enemy has Perished");
            enemyMain.OnDeath();
        }
    }
    
    public bool ApplyHeal(float _heal) {
        return false;
    }
}
