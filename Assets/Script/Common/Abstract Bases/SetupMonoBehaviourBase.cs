using System.Collections.Generic;
using System.Linq;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using ModestTree;
using UnityEngine;

namespace Common.Abstract_Bases
{
    public abstract class SetupMonoBehaviourBase : MonoBehaviour
    {
        private bool _needWaitOtherObjects;
        private List<IInitializable> _objectsToWaitBeforeInitialization;
        private bool _wasInitialized;
        private bool _wasStartPhasePassed;

        protected abstract IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization { get; }
        protected abstract void Prepare();
        protected abstract void Initialize();

        private void Awake()
        {
            Prepare();
            _objectsToWaitBeforeInitialization = new List<IInitializable>(ObjectsToWaitBeforeInitialization);
            _needWaitOtherObjects = !_objectsToWaitBeforeInitialization.IsEmpty();
        }

        private void Start()
        {
            if (!_needWaitOtherObjects || _needWaitOtherObjects && IsAllRequiredObjectsInitialized())
            {
                RunInitialization();
            }

            _wasStartPhasePassed = true;
        }

        private void OnEnable()
        {
            if (_wasInitialized || !_needWaitOtherObjects)
            {
                return;
            }

            SubscribeOnEvents();
        }

        private void OnDisable()
        {
            if (_wasInitialized || !_needWaitOtherObjects)
            {
                return;
            }

            UnsubscribeFromEvents();
        }

        private void SubscribeOnEvents()
        {
            foreach (IInitializable initializableObject in _objectsToWaitBeforeInitialization)
            {
                initializableObject.InitializationStatusChanged += OnInitializableObjectStatusChanged;
            }
        }

        private void UnsubscribeFromEvents()
        {
            foreach (IInitializable initializableObject in _objectsToWaitBeforeInitialization)
            {
                initializableObject.InitializationStatusChanged -= OnInitializableObjectStatusChanged;
            }
        }

        private void OnInitializableObjectStatusChanged(InitializationStatus obj)
        {
            if (_wasInitialized || !_wasStartPhasePassed)
            {
                return;
            }

            if (IsAllRequiredObjectsInitialized())
            {
                RunInitialization();
            }
        }

        private bool IsAllRequiredObjectsInitialized()
        {
            return _objectsToWaitBeforeInitialization.All(initializableObject =>
                initializableObject.CurrentInitializationStatus == InitializationStatus.Initialized);
        }

        private void RunInitialization()
        {
            Initialize();
            _wasInitialized = true;
            if (_needWaitOtherObjects)
            {
                UnsubscribeFromEvents();
            }
        }
    }
}