using System.Collections.Generic;
using System.Linq;
using Common.Abstract_Bases.Disableable;
using Common.Abstract_Bases.Factories.Position_Data_For_Instantiation;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using ModestTree;
using UnityEngine;
using Zenject;
using IInitializable = Common.Abstract_Bases.Initializable_MonoBehaviour.IInitializable;

namespace Common.Abstract_Bases.Factories.Object_Pool
{
    public abstract class
        ObjectPoolingFactoryWithInstantiatorBase<TPoolItem, TDataForActivation> : FactoryWithInstantiatorBase,
            IDisableable,
            IObjectPoolingFactory
        where TPoolItem : IObjectPoolItem<TDataForActivation> where TDataForActivation : IPositionDataForInstantiation
    {
        private readonly IPositionDataForInstantiation _defaultPositionDataForInstantiation;
        private readonly List<TPoolItem> _items;
        private readonly int _needItemsCount;
        private readonly Queue<TDataForActivation> _nextItemDataForActivations;
        private readonly IPrefabProvider _prefabProvider;
        private readonly List<TPoolItem> _waitingInitializationItems;

        protected ObjectPoolingFactoryWithInstantiatorBase(IInstantiator instantiator, Transform defaultParentTransform,
            int needItemsCount, IPrefabProvider prefabProvider,
            IPositionDataForInstantiation defaultPositionDataForInstantiation) : base(instantiator,
            defaultParentTransform)
        {
            _items = new List<TPoolItem>();
            _waitingInitializationItems = new List<TPoolItem>();
            _nextItemDataForActivations = new Queue<TDataForActivation>();
            _prefabProvider = prefabProvider;
            _needItemsCount = needItemsCount;
            _defaultPositionDataForInstantiation = defaultPositionDataForInstantiation;
        }

        public void Enable()
        {
            if (_items == null)
            {
                return;
            }

            foreach (TPoolItem objectPoolItem in _items)
            {
                objectPoolItem.Deactivated += OnItemDeactivated;
            }

            foreach (TPoolItem waitingInitializationItem in _waitingInitializationItems)
            {
                SubscribeOnWaitingInitializationItem(waitingInitializationItem);
            }
        }

        public void Disable()
        {
            if (_items == null)
            {
                return;
            }

            foreach (TPoolItem objectPoolItem in _items)
            {
                objectPoolItem.Deactivated -= OnItemDeactivated;
            }

            foreach (TPoolItem waitingInitializationItem in _waitingInitializationItems)
            {
                UnsubscribeFromWaitingInitializationItem(waitingInitializationItem);
            }
        }

        public void FillPool()
        {
            for (var i = 0; i < _needItemsCount; i++)
            {
                InstantiateNewItem();
            }
        }

        protected void Create(TDataForActivation dataForActivation)
        {
            if (_items.All(item => item.IsUsed))
            {
                _nextItemDataForActivations.Enqueue(dataForActivation);
                InstantiateNewItem();
            }
            else
            {
                TPoolItem freeItem = _items.Find(item => !item.IsUsed);
                freeItem.Activate(dataForActivation);
            }
        }

        private void SubscribeOnWaitingInitializationItem(IInitializable waitingInitializationItem)
        {
            waitingInitializationItem.InitializationStatusChanged += OnNewItemInitializationStatusChanged;
        }

        private void UnsubscribeFromWaitingInitializationItem(IInitializable waitingInitializationItem)
        {
            waitingInitializationItem.InitializationStatusChanged -= OnNewItemInitializationStatusChanged;
        }

        private void OnItemDeactivated(IObjectPoolItem<TDataForActivation> obj)
        {
            HandleNewFreeItem(obj);
        }

        private void OnNewItemInitializationStatusChanged(
            InitializableMonoBehaviourStatus newInitializableMonoBehaviourStatus)
        {
            if (newInitializableMonoBehaviourStatus == InitializableMonoBehaviourStatus.Initialized)
            {
                TPoolItem initializedItem = _waitingInitializationItems.First(item =>
                    item.CurrentInitializableMonoBehaviourStatus == InitializableMonoBehaviourStatus.Initialized);
                UnsubscribeFromWaitingInitializationItem(initializedItem);
                _waitingInitializationItems.Remove(initializedItem);
                _items.Add(initializedItem);
                HandleNewFreeItem(initializedItem);
            }
        }

        private void InstantiateNewItem()
        {
            var newItem =
                InstantiatePrefabForComponent<TPoolItem>(_prefabProvider, _defaultPositionDataForInstantiation);
            if (newItem.CurrentInitializableMonoBehaviourStatus == InitializableMonoBehaviourStatus.NonInitialized)
            {
                _waitingInitializationItems.Add(newItem);
                SubscribeOnWaitingInitializationItem(newItem);
            }
            else
            {
                _items.Add(newItem);
                HandleNewFreeItem(newItem);
            }
        }

        private void HandleNewFreeItem(IObjectPoolItem<TDataForActivation> newFreeItem)
        {
            if (_nextItemDataForActivations.IsEmpty())
            {
                return;
            }

            newFreeItem.Activate(_nextItemDataForActivations.Dequeue());
        }
    }
}