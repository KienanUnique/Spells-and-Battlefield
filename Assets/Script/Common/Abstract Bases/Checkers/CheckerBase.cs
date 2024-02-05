using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Common.Collider_With_Disabling;
using UnityEngine;

namespace Common.Abstract_Bases.Checkers
{
    public abstract class CheckerBase : InitializableMonoBehaviourBase, IChecker
    {
        private List<Collider> _colliders;
        private List<IReadonlyColliderWithDisabling> _collidersWithDisabling;
        private ValueWithReactionOnChange<bool> _isCollidingWithReaction;

        public event Action<bool> ContactStateChanged;

        public bool IsColliding => _isCollidingWithReaction.Value;
        public ReadOnlyCollection<Collider> Colliders => new ReadOnlyCollection<Collider>(_colliders);
        protected abstract LayerMask NeedObjectsMask { get; }
        protected abstract void SpecialAwakeAction();

        protected override void Awake()
        {
            base.Awake();
            _colliders = new List<Collider>();
            _collidersWithDisabling = new List<IReadonlyColliderWithDisabling>();
            _isCollidingWithReaction = new ValueWithReactionOnChange<bool>(false);
            SpecialAwakeAction();
            SetInitializedStatus();
        }

        protected override void SubscribeOnEvents()
        {
            _isCollidingWithReaction.AfterValueChanged += OnAfterCollidingStateChanged;
        }

        protected override void UnsubscribeFromEvents()
        {
            _isCollidingWithReaction.AfterValueChanged -= OnAfterCollidingStateChanged;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!IsNeedCollider(other))
            {
                return;
            }

            _colliders.Add(other);
            _isCollidingWithReaction.Value = true;

            if (!other.TryGetComponent(out IReadonlyColliderWithDisabling colliderWithDisabling))
            {
                return;
            }

            _collidersWithDisabling.Add(colliderWithDisabling);
            if (isActiveAndEnabled)
            {
                SubscribeOnColliderWithDisabling(colliderWithDisabling);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!IsNeedCollider(other))
            {
                return;
            }

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

        private void SubscribeOnColliderWithDisabling(IReadonlyColliderWithDisabling colliderWithDisabling)
        {
            colliderWithDisabling.Disabled += OnColliderDisabled;
        }

        private void UnsubscribeOnColliderWithDisabling(IReadonlyColliderWithDisabling colliderWithDisabling)
        {
            colliderWithDisabling.Disabled -= OnColliderDisabled;
        }

        private void OnColliderDisabled(IReadonlyColliderWithDisabling arg1, Collider arg2)
        {
            UnsubscribeOnColliderWithDisabling(arg1);
            _collidersWithDisabling.Remove(arg1);
            _colliders.Remove(arg2);
            if (_colliders.Count == 0)
            {
                _isCollidingWithReaction.Value = false;
            }
        }
    }
}