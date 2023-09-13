using System;
using System.Collections.Generic;
using System.Linq;
using Common.Abstract_Bases.Initializable_MonoBehaviour;

namespace Common.Abstract_Bases.Box_Collider_Trigger
{
    public abstract class TriggerForInitializableObjectsBase<TRequiredObject> : BoxColliderTriggerBase<TRequiredObject>
        where TRequiredObject : IInitializable
    {
        private readonly HashSet<TRequiredObject> _waitingInitializationObjects = new HashSet<TRequiredObject>();

        protected sealed override void OnRequiredObjectEnteringDetected(TRequiredObject requiredObject)
        {
            switch (requiredObject.CurrentInitializableMonoBehaviourStatus)
            {
                case InitializableMonoBehaviourStatus.NonInitialized:
                    _waitingInitializationObjects.Add(requiredObject);
                    if (isActiveAndEnabled)
                    {
                        SubscribeOnObject(requiredObject);
                    }

                    break;
                case InitializableMonoBehaviourStatus.Initialized:
                    base.OnRequiredObjectEnteringDetected(requiredObject);
                    if (isActiveAndEnabled)
                    {
                        SubscribeOnObject(requiredObject);
                    }

                    break;
            }
        }

        protected sealed override void OnRequiredObjectExitingDetected(TRequiredObject requiredObject)
        {
            base.OnRequiredObjectExitingDetected(requiredObject);
        }

        protected virtual void OnEnable()
        {
            foreach (TRequiredObject requiredObject in _requiredObjectsInside)
            {
                requiredObject.InitializationStatusChanged += OnObjectInitializationStatusChanged;
            }

            foreach (TRequiredObject waitingInitializationObject in _waitingInitializationObjects)
            {
                waitingInitializationObject.InitializationStatusChanged += OnObjectInitializationStatusChanged;
            }
        }

        protected virtual void OnDisable()
        {
            foreach (TRequiredObject requiredObject in _requiredObjectsInside)
            {
                requiredObject.InitializationStatusChanged -= OnObjectInitializationStatusChanged;
            }

            foreach (TRequiredObject waitingInitializationObject in _waitingInitializationObjects)
            {
                waitingInitializationObject.InitializationStatusChanged -= OnObjectInitializationStatusChanged;
            }
        }

        private void OnObjectInitializationStatusChanged(
            InitializableMonoBehaviourStatus newInitializableMonoBehaviourStatus)
        {
            switch (newInitializableMonoBehaviourStatus)
            {
                case InitializableMonoBehaviourStatus.NonInitialized:
                    break;
                case InitializableMonoBehaviourStatus.Initialized:

                    bool IsTargetInitialized(TRequiredObject requiredObject) =>
                        requiredObject.CurrentInitializableMonoBehaviourStatus ==
                        InitializableMonoBehaviourStatus.Initialized;

                    foreach (TRequiredObject requiredObject in _waitingInitializationObjects.Where(IsTargetInitialized))
                    {
                        UnsubscribeFromObject(requiredObject);
                        base.OnRequiredObjectEnteringDetected(requiredObject);
                    }

                    _waitingInitializationObjects.RemoveWhere(IsTargetInitialized);
                    break;
                case InitializableMonoBehaviourStatus.Destroying:

                    bool IsTargetDestroying(TRequiredObject requiredObject) =>
                        requiredObject.CurrentInitializableMonoBehaviourStatus ==
                        InitializableMonoBehaviourStatus.Destroying;

                    var destroyingObjects = new List<TRequiredObject>(_requiredObjectsInside.Where(IsTargetDestroying));
                    foreach (TRequiredObject requiredObject in destroyingObjects)
                    {
                        base.OnRequiredObjectExitingDetected(requiredObject);
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newInitializableMonoBehaviourStatus),
                        newInitializableMonoBehaviourStatus, null);
            }
        }

        private void SubscribeOnObject(IInitializable initializableObject)
        {
            initializableObject.InitializationStatusChanged += OnObjectInitializationStatusChanged;
        }

        private void UnsubscribeFromObject(IInitializable initializableObject)
        {
            initializableObject.InitializationStatusChanged -= OnObjectInitializationStatusChanged;
        }
    }
}