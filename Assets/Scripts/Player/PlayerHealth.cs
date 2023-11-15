using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth {
    [SerializeField]
    private float health;
    [SerializeField]
    private float maxHealth;
    public event Action<GameObject> OnDeathCallback;
    void Start() {
        
    }
    public float GetHealth() => health;
    public void ApplyDamage(float _damage) {
        health -= _damage;
        if (health < 0) {
            OnDeathCallback?.Invoke(gameObject);
        }
    }
    public bool ApplyHeal(float _heal) {
        if (health < maxHealth) {
            health += _heal;
            if (health > maxHealth)
                health = maxHealth;
            return true;
        }
        return false;
    }
}
