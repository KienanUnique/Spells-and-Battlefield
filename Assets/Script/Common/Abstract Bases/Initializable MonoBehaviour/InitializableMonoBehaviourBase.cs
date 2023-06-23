using System;
using System.Collections.Generic;
using Common.Abstract_Bases.Disableable;
using UnityEngine;

namespace Common.Abstract_Bases.Initializable_MonoBehaviour
{
    public abstract class InitializableMonoBehaviourBase : MonoBehaviour, IInitializable
    {
        private ValueWithReactionOnChange<InitializationStatus> _currentStatus;
        private List<IDisableable> _itemsNeedDisabling;

        public event Action<InitializationStatus> InitializationStatusChanged;

        public InitializationStatus CurrentInitializationStatus => _currentStatus.Value;

        protected virtual void SubscribeOnEvents()
        {
        }

        protected virtual void UnsubscribeFromEvents()
        {
        }

        protected virtual void Awake()
        {
            _currentStatus = new ValueWithReactionOnChange<InitializationStatus>(InitializationStatus.NonInitialized);
            _itemsNeedDisabling = null;
        }

        protected virtual void OnEnable()
        {
            _currentStatus.AfterValueChanged += OnInitializationStatusChanged;
            if (_currentStatus.Value != InitializationStatus.NonInitialized)
            {
                SubscribeOnEvents();
                _itemsNeedDisabling?.ForEach(item => item.Enable());
            }
        }

        protected virtual void OnDisable()
        {
            _currentStatus.AfterValueChanged -= OnInitializationStatusChanged;
            UnsubscribeFromEvents();
            _itemsNeedDisabling?.ForEach(item => item.Disable());
        }

        protected void SetInitializedStatus()
        {
            SubscribeOnEvents();
            _currentStatus.Value = InitializationStatus.Initialized;
        }

        protected void SetItemsNeedDisabling(List<IDisableable> itemsNeedDisabling)
        {
            _itemsNeedDisabling = itemsNeedDisabling;
        }

        private void OnInitializationStatusChanged(InitializationStatus newStatus)
        {
            InitializationStatusChanged?.Invoke(newStatus);
        }
    }
}