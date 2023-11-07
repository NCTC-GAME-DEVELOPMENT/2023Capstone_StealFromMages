using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IHealth {
    [SerializeField]
    private float health;    
    public event Action<GameObject> OnDeathCallback;
    public float GetHealth() => health;
    public void ApplyDamage(float _damage) {
        health -= _damage;
        if (health < 0) {
            OnDeathCallback.Invoke(gameObject);
        }
    }
    public void ApplyHeal(float _heal) {
        
    }
}
