using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamagable {
    [SerializeField]
    private float health;    
    public event Action<GameObject> OnDeathCallback;
    public float GetHealth() => health;
    public void OnDamage(float _damage) {
        health -= _damage;
        if (health < 0) {
            OnDeathCallback.Invoke(gameObject);
        }
    }
}
