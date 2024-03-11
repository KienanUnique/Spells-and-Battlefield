using System;
using UnityEngine;

namespace Spells.Collision_Collider
{
    [RequireComponent(typeof(Collider))]
    public class ColliderWithMasksChecking : MonoBehaviour, ISpellCollisionTrigger
    {
        [SerializeField] private LayerMask _mask;

        public event Action<Collider> TriggerEntered;

        private void OnTriggerEnter(Collider other)
        {
            if (IsNeedCollider(other))
            {
                TriggerEntered?.Invoke(other);
            }
        }

        private bool IsNeedCollider(Component colliderToCheck)
        {
            return (_mask.value & (1 << colliderToCheck.gameObject.layer)) > 0;
        }
    }
}