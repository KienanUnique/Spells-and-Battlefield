using System.Collections.Generic;
using UnityEngine;

namespace Game_Managers
{
    public class InteractableGameObjectsBankOfIds : MonoBehaviour
    {
        public static InteractableGameObjectsBankOfIds Instance { get; private set; }
        private const int FirstId = 0;
        private List<int> _usedIds;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }

            _usedIds = new List<int>();
        }

        public int GetId()
        {
            int freeId = FirstId;
            while (_usedIds.Contains(freeId))
            {
                freeId++;
            }

            _usedIds.Add(freeId);
            return freeId;
        }

        public void ReturnId(int returnedId)
        {
            _usedIds.Remove(returnedId);
        }
    }
}