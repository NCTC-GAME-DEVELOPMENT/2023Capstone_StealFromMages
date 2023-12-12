using UnityEngine;

public abstract class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviourSingleton<T>
{
    [SerializeField]
    protected bool isPersistent = true;
    public static T Instance { get; protected set; }
    public virtual void Awake() {
        if (Instance != null && Instance != this) {
#if UNITY_EDITOR
            Debug.Log(string.Format("Duplicate Singleton : {0}. Current Singleton Instance is {1}" , this.name, Instance.name));
#endif
            Destroy(this);
        }
        else {
#if UNITY_EDITOR
            Debug.Log(string.Format("Created Singleton : {0} inside {1}" , GetType().Name ,this.name));
#endif
            Instance = (T)this;
            if (isPersistent)
                DontDestroyOnLoad((T)this);
        }       
    }
    private void OnDestroy()
    {
        if (this == Instance) {
            Instance = null;
        }
#if UNITY_EDITOR
        Debug.Log("Deleted Singleton : " + this.name);
#endif
    }
}
