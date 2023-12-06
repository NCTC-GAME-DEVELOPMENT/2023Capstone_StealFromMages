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

    //Animator public var 
    public Animator animator;
    private float currentHealth;

    void Update()
    {
        //connect actual health to health param for animator: checks health and plays death anim
        animator.SetFloat("Health", health);
    }

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        currentHealth = health;
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
        else
        {
            //hurt anim
            animator.SetTrigger("tookDamage");
            //get out of hurt anim
            StartCoroutine(ReturnToDefaultStateAfterDelay(1.0f));
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
        if (collision.tag == "HealthPotion" && health != maxHealth) {
            ApplyHeal(10);
            collision.gameObject.SetActive(false);
        }
    }


    //get animation from hurt back to run/idle
    IEnumerator ReturnToDefaultStateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetTrigger("ReturnToDefault");
    }

}
