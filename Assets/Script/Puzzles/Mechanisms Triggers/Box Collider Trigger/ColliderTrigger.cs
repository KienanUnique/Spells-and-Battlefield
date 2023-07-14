using System;
using UnityEngine;

namespace Puzzles.Mechanisms_Triggers.Box_Collider_Trigger
{
    [RequireComponent(typeof(Collider))]
    public class ColliderTrigger : MonoBehaviour, IColliderTrigger
    {
        public event Action<Collider> TriggerEnter;
        public event Action<Collider> TriggerExit;
        private void OnTriggerEnter(Collider other)
        {
            TriggerEnter?.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            TriggerExit?.Invoke(other);
        }
    }
}