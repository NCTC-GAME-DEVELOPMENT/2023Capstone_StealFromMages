using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightStart : MonoBehaviour
{
    public GameObject WallCollision;
    public GameObject VineWall; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // what is that... 
        //Debug.Log("BFS: " + collision.name);

        PlayerController pc = collision.gameObject.GetComponent<PlayerController>(); 
        if (pc)
        {
            // what is that... 
            Debug.Log("BFS: " + collision.name);
            WallCollision.SetActive(true);
            VineWall.SetActive(true);
            gameObject.SetActive(false); 
        }



    }
}
