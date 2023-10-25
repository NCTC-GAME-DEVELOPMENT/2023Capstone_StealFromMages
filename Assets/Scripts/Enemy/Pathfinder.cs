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
    private static Transform playerTransform;
    [SerializeField]
    private List<EnemyMovement> enemyTransforms = new List<EnemyMovement>();
    public override void Awake() {
        base.Awake();
    }
    public void Start() {
        playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
        IsPlayerInAggroDistance();
    }
    public void FixedUpdate() {
        //IsPlayerInAggroDistance();
    }
    public static Vector3 GetPlayerPosition() => playerTransform.position;
    public void IsPlayerInAggroDistance() {
        if (playerTransform == null) {
            Debug.Log("Player not Found, trying to find New Player");
            playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;       
        }
        else 
            foreach (EnemyMovement enemy in enemyTransforms)
            {
                if (Vector3.Distance(playerTransform.position, enemy.GetPosition()) < enemy.AggroDistance)
                    enemy.SwitchAggroStance();
                else
                    enemy.SwitchPassiveStance();
            }
        TickSystem.Instance?.CreateTimer(IsPlayerInAggroDistance, (uint)5);
        Debug.Log("PlayerCheck");
    }

}
