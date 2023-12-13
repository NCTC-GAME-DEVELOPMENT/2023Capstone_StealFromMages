using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour, IHealth {
    [SerializeField]
    private float health;
    [SerializeField]
    private float maxHealth;
    public event Action<GameObject> OnDeathCallback;
    public event Action OnPlayerHealthUpdate;


    //Animator public var 
    public Animator animator;
    private float currentHealth;

    //audio
    public AudioClip playerDamage;
    public AudioSource audioSource;

    void Update()
    {
        //connect actual health to health param for animator: checks health and plays death anim
        animator.SetFloat("Health", health);
    }

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        currentHealth = health;

        //assign audio source
        audioSource = GetComponent<AudioSource>();
    }
    public float GetHealthFill() {
        return health / maxHealth;
    }
    public float GetHealth() => health;
    public float GetMaxHealth() => maxHealth;
    public void ApplyDamage(float _damage) {
        Debug.Log("Took Damage");
        health -= _damage;
        audioSource.PlayOneShot(playerDamage);
        OnPlayerHealthUpdate?.Invoke();
        if (health < .1) {
            OnDeathCallback?.Invoke(gameObject);
            Pathfinder.Instance.gameObject.SetActive(false);
            gameObject.GetComponent<PlayerController>().moveSpeed = 0;
            TickSystem.Instance.CreateTimer(OnDeath, 2);
        }
        else
        {
            //hurt anim
            animator.SetTrigger("tookDamage");
            //get out of hurt anim
            StartCoroutine(ReturnToDefaultStateAfterDelay(0.7f));


        }
    }
    public bool ApplyHeal(float _heal) {
        OnPlayerHealthUpdate?.Invoke();
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

    private void OnDeath() {
        SceneManager.LoadScene(7);
    }


    //get animation from hurt back to run/idle
    IEnumerator ReturnToDefaultStateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetTrigger("ReturnToDefault");
    }

}
