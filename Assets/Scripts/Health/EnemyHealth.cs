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

    //Animator public var 
    public Animator animator;


    //audio
    public AudioClip slimeDamage;
    public AudioClip slimeDeath;
    private AudioSource audioSource;

    void Update()
    {
        //connect actual health to health param for animator: checks health and plays death anim
        if(animator != null)
        animator.SetFloat("Health", health);
    }
    void Start () {
    enemyMain = GetComponent<EnemyMain>();
        //assign audio source
        audioSource = GetComponent<AudioSource>();
    }
    public float GetHealth() => health;
    public void ApplyDamage(float _damage) {
        Debug.Log(_damage + " Damage");
        audioSource.PlayOneShot(slimeDamage);
        health -= _damage;
        if (health < 0) {
            audioSource.PlayOneShot(slimeDeath);
            OnDeathCallback?.Invoke(gameObject);
            Debug.Log("Enemy has Perished");
            enemyMain.OnDeath();
        }
        else
        {
            //hurt anim
            animator.SetTrigger("TookDamage");
            //get out of hurt anim
            StartCoroutine(ReturnToDefaultStateAfterDelay(0.5f));
        }
    }
    
    public bool ApplyHeal(float _heal) {
        return false;
    }

    //get animation from hurt back to run/idle
    IEnumerator ReturnToDefaultStateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetTrigger("ReturnToDefault");
    }

}
