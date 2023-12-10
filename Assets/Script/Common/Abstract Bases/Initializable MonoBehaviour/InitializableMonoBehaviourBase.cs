using System;
using System.Collections.Generic;
using Common.Abstract_Bases.Disableable;
using UnityEngine;

namespace Common.Abstract_Bases.Initializable_MonoBehaviour
{
    public abstract class InitializableMonoBehaviourBase : MonoBehaviour, IInitializableWithActionsPool
    {
        private readonly List<Action> _actionsAfterInitialization = new List<Action>();

        private readonly ValueWithReactionOnChange<InitializableMonoBehaviourStatus> _currentStatus =
            new ValueWithReactionOnChange<InitializableMonoBehaviourStatus>(InitializableMonoBehaviourStatus
                .NonInitialized);

        private List<IDisableable> _itemsNeedDisabling;

        public event Action<InitializableMonoBehaviourStatus> InitializationStatusChanged;

        public InitializableMonoBehaviourStatus CurrentInitializableMonoBehaviourStatus => _currentStatus.Value;

        public void AddActionAfterInitializing(Action action)
        {
            if (CurrentInitializableMonoBehaviourStatus == InitializableMonoBehaviourStatus.NonInitialized)
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

        protected virtual void OnInitialized()
        {
        }

        protected virtual void OnEnable()
        {
            if (_currentStatus.Value != InitializableMonoBehaviourStatus.NonInitialized)
            {
                SubscribeOnBaseEvents();
            }
        }

        protected virtual void OnDisable()
        {
            if (_currentStatus.Value != InitializableMonoBehaviourStatus.NonInitialized)
            {
                UnsubscribeFromBaseEvents();
            }
        }

        protected virtual void OnDestroy()
        {
            _currentStatus.Value = InitializableMonoBehaviourStatus.Destroying;
            InitializationStatusChanged?.Invoke(InitializableMonoBehaviourStatus.Destroying);
        }

        protected void SetInitializedStatus()
        {
            SubscribeOnBaseEvents();
            _currentStatus.Value = InitializableMonoBehaviourStatus.Initialized;
            foreach (Action action in _actionsAfterInitialization)
            {
                action.Invoke();
            }

            _actionsAfterInitialization.Clear();
            OnInitialized();
        }

        protected void SetItemsNeedDisabling(List<IDisableable> itemsNeedDisabling)
        {
            _itemsNeedDisabling = itemsNeedDisabling;
        }

        protected void AddItemsNeedDisabling(IEnumerable<IDisableable> itemsNeedDisabling)
        {
            _itemsNeedDisabling.AddRange(itemsNeedDisabling);
        }

        protected void AddItemNeedDisabling(IDisableable itemNeedDisabling)
        {
            _itemsNeedDisabling.Add(itemNeedDisabling);
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

        private void OnInitializationStatusChanged(InitializableMonoBehaviourStatus newStatus)
        {
            InitializationStatusChanged?.Invoke(newStatus);
        }
    }
}