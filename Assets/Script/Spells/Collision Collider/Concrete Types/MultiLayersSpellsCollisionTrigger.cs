using System;
using System.Collections.Generic;
using Puzzles.Mechanisms_Triggers.Box_Collider_Trigger;
using UnityEngine;

namespace Spells.Collision_Collider.Concrete_Types
{
    public class MultiLayersSpellsCollisionTrigger : SpellCollisionTriggerBase
    {
        [SerializeField] private List<ColliderWithMasksChecking> _collidersWithMasks;
        [SerializeField] private List<ColliderTrigger> _defaultColliders;
        
        public override event Action<Collider> TriggerEntered;

        private void OnEnable()
        {
            foreach (var collidersWithMask in _collidersWithMasks)
            {
                collidersWithMask.TriggerEntered += OnTriggerEntered;
            }
            
            foreach (var defaultCollider in _defaultColliders)
            {
                defaultCollider.TriggerEnter += OnTriggerEntered;
            }
        }

        private void OnDisable()
        {
            foreach (var collidersWithMask in _collidersWithMasks)
            {
                collidersWithMask.TriggerEntered -= OnTriggerEntered;
            }
            
            foreach (var defaultCollider in _defaultColliders)
            {
                defaultCollider.TriggerEnter -= OnTriggerEntered;
            }
        }

        private void OnTriggerEntered(Collider other)
        {
            TriggerEntered?.Invoke(other);
        }
    }
}