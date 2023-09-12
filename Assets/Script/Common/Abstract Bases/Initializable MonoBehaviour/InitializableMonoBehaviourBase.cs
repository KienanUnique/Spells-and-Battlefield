using System;
using System.Collections.Generic;
using Common.Abstract_Bases.Disableable;
using UnityEngine;

namespace Common.Abstract_Bases.Initializable_MonoBehaviour
{
    public abstract class InitializableMonoBehaviourBase : MonoBehaviour, IInitializableWithActionsPool
    {
        private readonly List<Action> _actionsAfterInitialization = new List<Action>();

        private readonly ValueWithReactionOnChange<InitializationStatus> _currentStatus =
            new ValueWithReactionOnChange<InitializationStatus>(InitializationStatus.NonInitialized);

        private List<IDisableable> _itemsNeedDisabling;

        public event Action<InitializationStatus> InitializationStatusChanged;

        public InitializationStatus CurrentInitializationStatus => _currentStatus.Value;

        public void AddActionAfterInitializing(Action action)
        {
            if (CurrentInitializationStatus == InitializationStatus.NonInitialized)
            {
                _actionsAfterInitialization.Add(action);
            }
            else
            {
                action.Invoke();
            }
        }

        protected abstract void SubscribeOnEvents();
        protected abstract void UnsubscribeFromEvents();

        protected virtual void Awake()
        {
        }

        protected virtual void OnEnable()
        {
            if (_currentStatus.Value != InitializationStatus.NonInitialized)
            {
                SubscribeOnBaseEvents();
            }
        }

        protected virtual void OnDisable()
        {
            if (_currentStatus.Value != InitializationStatus.NonInitialized)
            {
                UnsubscribeFromBaseEvents();
            }
        }

        protected void SetInitializedStatus()
        {
            SubscribeOnBaseEvents();
            _currentStatus.Value = InitializationStatus.Initialized;
            foreach (Action action in _actionsAfterInitialization)
            {
                action.Invoke();
            }

            _actionsAfterInitialization.Clear();
        }

        protected void SetItemsNeedDisabling(List<IDisableable> itemsNeedDisabling)
        {
            _itemsNeedDisabling = itemsNeedDisabling;
        }

        private void SubscribeOnBaseEvents()
        {
            _currentStatus.AfterValueChanged += OnInitializationStatusChanged;
            _itemsNeedDisabling?.ForEach(item => item.Enable());
            SubscribeOnEvents();
        }

        private void UnsubscribeFromBaseEvents()
        {
            _currentStatus.AfterValueChanged -= OnInitializationStatusChanged;
            _itemsNeedDisabling?.ForEach(item => item.Disable());
            UnsubscribeFromEvents();
        }

        private void OnInitializationStatusChanged(InitializationStatus newStatus)
        {
            InitializationStatusChanged?.Invoke(newStatus);
        }
    }
}