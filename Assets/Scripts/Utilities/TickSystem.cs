using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class TickSystem : ISingleton<TickSystem>,IDisposable, IInitialize {
    private const short DEFAULT_TPS = 20;
    public static short TPS;//Ticks Per Second
    private static float TPSDecimal;
    protected uint currentTick;
    private float TimePassedSinceLastTick;
    private List<TickTimer> TimerList = new List<TickTimer>();
    public delegate void TimerCallback();// Generic Struct maybe???? Allows for Versatility???? Future Stuff
    private static event Action<uint> TickChangeEvent;
    public static TickSystem Instance {
        get; private set;
    }
    public static void SubscribeToTickChange(Action<uint> _subscriber) => TickChangeEvent += _subscriber;
    public static void UnSubscribeToTickChange(Action<uint> _subscriber) => TickChangeEvent -= _subscriber;
    public void CreateTimer(TimerCallback _timerCallback, int _timeInSeconds) => TimerList.Add(new TickTimer(_timerCallback, _timeInSeconds));
    public void CreateTimer(TimerCallback _timerCallback, uint _timeInTicks) => TimerList.Add(new TickTimer(_timerCallback, _timeInTicks));
    private class TickTimer {
        private readonly uint triggerTick;
        private TimerCallback timerCallback;
        public TickTimer(TimerCallback _timerCallback, int _timeInSeconds) {
            timerCallback = _timerCallback;
            int apporximateTPS = _timeInSeconds * TPS;
            triggerTick = (uint)apporximateTPS + TickSystem.Instance.GetCurrentTick();
        }
        public TickTimer(TimerCallback _timerCallback, uint _timeInTicks) {
            timerCallback = _timerCallback;
            triggerTick = TickSystem.Instance.GetCurrentTick() + _timeInTicks;
        }
        public bool CheckTick(uint tick) {
            if (tick < triggerTick)
                return false;
            timerCallback();
            return true;
        }

    }
    internal void ProgressTick() {
        TimePassedSinceLastTick += Time.deltaTime;
        if (TimePassedSinceLastTick > TPSDecimal) {
            TimePassedSinceLastTick -= TPSDecimal;
            currentTick++;
            TickChangeEvent?.Invoke(currentTick);
        }
    }
    public uint GetCurrentTick() => currentTick;
    public void Dispose() {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    protected virtual void Dispose(bool _disposing) {
        if (_disposing) {
            // Implement handling of Managed and Unmanaged Resources
        }
    }
    private void CheckTimerList(uint _tick) {
        List<TickTimer> timerList = new List<TickTimer>(TimerList);
        List<TickTimer> RemovalList = new List<TickTimer>();
        foreach (TickTimer tickTimer in timerList)
            if (tickTimer.CheckTick(_tick))
                RemovalList.Add(tickTimer);
        foreach (TickTimer tickTimer in RemovalList)
            TimerList.Remove(tickTimer);
    }
    public void InitializeMain() {
        if (TimerList == null)
            TimerList = new List<TickTimer>();
        TPS = DEFAULT_TPS;
        currentTick = 0;
        TickChangeEvent += CheckTimerList;
        TPSDecimal = 1f / (float)TPS;
        if (Instance == null || Instance != this)
            Instance = this;

    }
}



