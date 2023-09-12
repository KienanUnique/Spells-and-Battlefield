using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Abstract_Bases.Box_Collider_Trigger
{
    [RequireComponent(typeof(BoxCollider))]
    public abstract class BoxColliderTriggerBase<TRequiredObject> : MonoBehaviour
    {
        protected List<TRequiredObject> _requiredObjectsInside;

        protected event Action<TRequiredObject> RequiredObjectEnteringDetected;
        protected event Action<TRequiredObject> RequiredObjectExitingDetected;

        protected virtual void OnRequiredObjectEnteringDetected()
        {
        }

        protected virtual void OnRequiredObjectExitingDetected()
        {
        }

        protected IReadOnlyCollection<TRequiredObject> GetRequiredObjectsInCollider()
        {
            return _requiredObjectsInside;
        }

        private void Awake()
        {
            _requiredObjectsInside = new List<TRequiredObject>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out TRequiredObject requiredObject))
            {
                _requiredObjectsInside.Add(requiredObject);
                OnRequiredObjectEnteringDetected();
                RequiredObjectEnteringDetected?.Invoke(requiredObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out TRequiredObject requiredObject) &&
                _requiredObjectsInside.Contains(requiredObject))
            {
                _requiredObjectsInside.Remove(requiredObject);
                OnRequiredObjectExitingDetected();
                RequiredObjectExitingDetected?.Invoke(requiredObject);
            }
        }
    }
}