using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class bullet : MonoBehaviour
{
    public float speed = 10f;
    public float bullet_lifespan = 0.5f;

    private Rigidbody2D bullet_Rigidbody2D
        ;
    // Start is called before the first frame update
    void Awake()
    {
        bullet_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        bullet_Rigidbody2D.velocity = transform.right * speed;
        Destroy(gameObject, bullet_lifespan);
    }

}
