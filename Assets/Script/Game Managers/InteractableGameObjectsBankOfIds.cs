using System.Collections.Generic;
using UnityEngine;

namespace Game_Managers
{
    public class InteractableGameObjectsBankOfIds : Singleton<InteractableGameObjectsBankOfIds>
    {
        private const int FirstId = 0;

        private List<int> _usedIds;

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


        protected override void SpecialAwakeAction()
        {
            _usedIds = new List<int>();
        }
    }
}