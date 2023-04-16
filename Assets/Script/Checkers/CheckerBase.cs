using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Checkers
{
    public abstract class CheckerBase : MonoBehaviour
    {
        public event Action<bool> ContactStateChanged;
        public bool IsColliding => _isCollidingWithReaction.Value;
        public ReadOnlyCollection<Collider> Colliders => new ReadOnlyCollection<Collider>(_colliders);
        protected abstract LayerMask NeedObjectsMask { get; }
        private ValueWithReactionOnChange<bool> _isCollidingWithReaction;
        private List<Collider> _colliders;

        private void Awake()
        {
            _colliders = new List<Collider>();
            _isCollidingWithReaction = new ValueWithReactionOnChange<bool>(false);
        }

        private void OnEnable()
        {
            _isCollidingWithReaction.AfterValueChanged += OnAfterGroundedStateChanged;
        }

        private void OnDisable()
        {
            _isCollidingWithReaction.AfterValueChanged -= OnAfterGroundedStateChanged;
        }

        private void OnAfterGroundedStateChanged(bool newIsGrounded)
        {
            ContactStateChanged?.Invoke(newIsGrounded);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (IsGroundCollider(other))
            {
                _colliders.Add(other);
                if (!IsColliding)
                {
                    _isCollidingWithReaction.Value = true;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!IsGroundCollider(other)) return;

            _colliders.Remove(other);
            if (_colliders.Count == 0 && IsColliding)
            {
                _isCollidingWithReaction.Value = false;
            }
        }

        private bool IsGroundCollider(Collider colliderToCheck)
        {
            return (NeedObjectsMask.value & (1 << colliderToCheck.gameObject.layer)) > 0;
        }
    }
}