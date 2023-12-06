using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerPetalHealth : MonoBehaviour, IHealth
{
    [SerializeField]
    private float health;
    public event Action<GameObject> OnDeathCallback;

    //Animator public var (drag animator already attached to player to this field in editor)
    public Animator animator;

    void Update() {
        //if enemy is dead, play death anim
        if (animator != null) {
            animator.SetFloat("Health", health);
        }
    }
    void Start() {
    }
    public float GetHealth() => health;
    public void ApplyDamage(float _damage) {
        Debug.Log(_damage + " Damage");
        health -= _damage;
        if (health <= 0) {
            OnDeathCallback?.Invoke(gameObject);
            Debug.Log("Petal has Perished");
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        }
    }

    public bool ApplyHeal(float _heal) {
        return false;
    }
}
