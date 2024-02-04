using System.Collections.Generic;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Puzzles.Mechanisms_Triggers;
using UnityEngine;

namespace Puzzles.Mechanisms
{
    public abstract class MechanismControllerBase : InitializableMonoBehaviourBase
    {
        private readonly List<IMechanismsTrigger> _triggers = new List<IMechanismsTrigger>();

        protected bool IsBusy { get; private set; }

        protected abstract void StartJob();

        protected void AddTriggers(List<IMechanismsTrigger> triggers)
        {
            Debug.Log($"triggers added = {triggers.Count}");
            _triggers.AddRange(triggers);
            
            if (CurrentInitializableMonoBehaviourStatus != InitializableMonoBehaviourStatus.Initialized ||
                !isActiveAndEnabled)
            {
                return;
            }

            foreach (IMechanismsTrigger trigger in triggers)
            {
                trigger.Triggered += OnTriggered;
            }
        }

        protected override void SubscribeOnEvents()
        {
            Debug.Log($"subscribed ({gameObject.name}), IsBusy = {IsBusy}, _triggers count = {_triggers.Count}");
            foreach (IMechanismsTrigger trigger in _triggers)
            {
                trigger.Triggered += OnTriggered;
            }
        }

        protected override void UnsubscribeFromEvents()
        {
            foreach (IMechanismsTrigger trigger in _triggers)
            {
                trigger.Triggered -= OnTriggered;
            }
        }

        protected virtual void OnTriggered()
        {
            Debug.Log($"OnTriggered, IsBusy = {IsBusy}");
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;
            StartJob();
        }

        protected void HandleDoneJob()
        {
            IsBusy = false;
        }
    }
}