using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Abstract_Bases.Box_Collider_Trigger
{
    [RequireComponent(typeof(Collider))]
    public abstract class ColliderTriggerBase<TRequiredObject> : MonoBehaviour
    {
        protected readonly List<TRequiredObject> _requiredObjectsInside = new();
        protected event Action<TRequiredObject> RequiredObjectEnteringDetected;
        protected event Action<TRequiredObject> RequiredObjectExitingDetected;

        protected virtual void OnRequiredObjectEnteringDetected(TRequiredObject requiredObject)
        {
            _requiredObjectsInside.Add(requiredObject);
            RequiredObjectEnteringDetected?.Invoke(requiredObject);
        }

        protected virtual void OnRequiredObjectExitingDetected(TRequiredObject requiredObject)
        {
            _requiredObjectsInside.Remove(requiredObject);
            RequiredObjectExitingDetected?.Invoke(requiredObject);
        }

        protected IReadOnlyCollection<TRequiredObject> GetRequiredObjectsInCollider()
        {
            return _requiredObjectsInside;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out TRequiredObject requiredObject))
            {
                OnRequiredObjectEnteringDetected(requiredObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out TRequiredObject requiredObject) &&
                _requiredObjectsInside.Contains(requiredObject))
            {
                OnRequiredObjectExitingDetected(requiredObject);
            }
        }
    }
}