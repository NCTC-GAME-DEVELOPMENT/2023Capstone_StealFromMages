using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviourSingleton<GameHandler>
{
    private TickSystem tickSystem;
    public TickSystem TickManager
    {
        get => tickSystem;
    }
    public override void Awake() {
        isPersistent = true;
        base.Awake();
        tickSystem = new TickSystem(
            // Will Implement In Time
            );
        tickSystem.InitializeMain();

    }
    public void FixedUpdate() { 
        tickSystem.ProgressTick();
        Debug.Log("Cuurent Tick is " + tickSystem.GetCurrentTick());
    }
}
