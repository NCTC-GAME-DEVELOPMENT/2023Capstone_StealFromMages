using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class ProjectileBase : MonoBehaviour {
    [SerializeField]
    protected float projectileLifetime;
    [SerializeField]
    protected float projectileSpeed;
    protected new Rigidbody2D rigidbody;
    void Awake() {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }
    void Update() {

    }
}
