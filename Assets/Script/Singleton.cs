using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }
    protected abstract T ThisInstance { get; }
    protected abstract void SpecialAwakeAction();

    private void Awake()
    {
        if (Instance != null && Instance != ThisInstance)
        {
            Destroy(this);
        }
        else
        {
            Instance = ThisInstance;
        }

        SpecialAwakeAction();
    }
}