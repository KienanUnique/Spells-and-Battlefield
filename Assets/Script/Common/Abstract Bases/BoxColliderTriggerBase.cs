using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Abstract_Bases
{
    [RequireComponent(typeof(BoxCollider))]
    public abstract class BoxColliderTriggerBase<TRequiredObject> : MonoBehaviour
    {
        protected List<TRequiredObject> _requiredObjectsInside;
        protected event Action<TRequiredObject> RequiredObjectEnteringDetected;
        protected event Action<TRequiredObject> RequiredObjectExitingDetected;
        protected IReadOnlyCollection<TRequiredObject> GetRequiredObjectsInCollider() => _requiredObjectsInside;

        private void Awake()
        {
            _requiredObjectsInside = new List<TRequiredObject>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out TRequiredObject requiredObject))
            {
                _requiredObjectsInside.Add(requiredObject);
                RequiredObjectEnteringDetected?.Invoke(requiredObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out TRequiredObject requiredObject) &&
                _requiredObjectsInside.Contains(requiredObject))
            {
                _requiredObjectsInside.Remove(requiredObject);
                RequiredObjectExitingDetected?.Invoke(requiredObject);
            }
        }
    }
}