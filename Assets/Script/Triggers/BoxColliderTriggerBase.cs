using System;
using UnityEngine;

namespace Triggers
{
    [RequireComponent(typeof(BoxCollider))]
    public abstract class BoxColliderTriggerBase<TRequiredObject> : MonoBehaviour
    {
        public Action<TRequiredObject> RequiredObjectEnteringDetected;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out TRequiredObject detectedObject))
            {
                RequiredObjectEnteringDetected?.Invoke(detectedObject);
            }
        }
    }
}