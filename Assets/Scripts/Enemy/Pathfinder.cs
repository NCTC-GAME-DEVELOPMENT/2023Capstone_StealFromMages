using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Pathfinder : MonoBehaviourSingleton<Pathfinder>
{
    /* Todo
     * Have List that Updates On Scene Change full of Enemies and Everyso often Check Distance between Player
     */
    [SerializeField]
    private GameObject player;
    [SerializeField]
    public override void Awake() {
        base.Awake();
    }
    public void Start() {
    }
   
    public void FixedUpdate() {
        
    }
    public Vector2 GetPlayerPosition() {
        if(player == null) {
            player = GameObject.FindWithTag("Player");
        }
        return player.transform.position;
    }

}
