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

    //Animator public var (drag animator already attached to player to this field in editor)
    public Animator animator;

    void Update()
    {
        //if player is dead, play death anim
        animator.SetFloat("Health", health);
    }

    void Start() {
        
    }
    public float GetHealthFill() {
        return health / maxHealth;
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
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "HealthPotion") {
            ApplyHeal(10);
        }
    }
}
