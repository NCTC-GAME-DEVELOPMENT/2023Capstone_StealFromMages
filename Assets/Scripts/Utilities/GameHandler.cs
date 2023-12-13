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
    }
    public void Start() {
        tickSystem = new TickSystem(
            // Will Implement In Time
            );
        tickSystem.InitializeMain();

    
    }
    public void OnDestroy() {
        tickSystem.Dispose();
    }
    public void FixedUpdate() { 
        tickSystem.ProgressTick();
    }
}
