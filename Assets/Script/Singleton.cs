using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }
    protected abstract void SpecialAwakeAction();

    private void Awake()
    {
        var thisInstanceAsT = this as T;
        if (Instance != null && Instance != thisInstanceAsT)
        {
            Destroy(this);
        }
        else
        {
            Instance = thisInstanceAsT;
        }

        SpecialAwakeAction();
    }
}