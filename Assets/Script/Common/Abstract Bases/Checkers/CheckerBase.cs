using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using UnityEngine;

namespace Common.Abstract_Bases.Checkers
{
    public abstract class CheckerBase : InitializableMonoBehaviourBase, IChecker
    {
        private ValueWithReactionOnChange<bool> _isCollidingWithReaction;
        private List<Collider> _colliders;

        public event Action<bool> ContactStateChanged;

        public bool IsColliding => _isCollidingWithReaction.Value;
        public ReadOnlyCollection<Collider> Colliders => new ReadOnlyCollection<Collider>(_colliders);
        protected abstract LayerMask NeedObjectsMask { get; }
        protected abstract void SpecialAwakeAction();

        protected override void SubscribeOnEvents()
        {
            _isCollidingWithReaction.AfterValueChanged += OnAfterCollidingStateChanged;
        }

        protected override void UnsubscribeFromEvents()
        {
            _isCollidingWithReaction.AfterValueChanged -= OnAfterCollidingStateChanged;
        }

        protected override void Awake()
        {
            base.Awake();
            _colliders = new List<Collider>();
            _isCollidingWithReaction = new ValueWithReactionOnChange<bool>(false);
            SpecialAwakeAction();
            SetInitializedStatus();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (IsNeedCollider(other))
            {
                _colliders.Add(other);
                _isCollidingWithReaction.Value = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!IsNeedCollider(other)) return;
            _colliders.Remove(other);
            if (_colliders.Count == 0)
            {
                _isCollidingWithReaction.Value = false;
            }
        }

        private void OnAfterCollidingStateChanged(bool isColliding)
        {
            ContactStateChanged?.Invoke(isColliding);
        }

        private bool IsNeedCollider(Collider colliderToCheck)
        {
            return (NeedObjectsMask.value & (1 << colliderToCheck.gameObject.layer)) > 0;
        }
    }
}