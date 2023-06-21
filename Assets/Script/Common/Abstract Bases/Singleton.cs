using UnityEngine;

namespace Common.Abstract_Bases
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T Instance { get; set; }

        protected virtual void SpecialAwakeAction()
        {
        }

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
}