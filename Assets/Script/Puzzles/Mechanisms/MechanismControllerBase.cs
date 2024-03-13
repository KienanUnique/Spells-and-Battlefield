using System;
using System.Collections.Generic;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Puzzles.Mechanisms_Triggers;

namespace Puzzles.Mechanisms
{
    public abstract class MechanismControllerBase : InitializableMonoBehaviourBase, IMechanismController
    {
        private readonly List<IMechanismsTrigger> _triggers = new();

        public event Action JobStarted;
        public event Action JobEnded;

        protected bool IsBusy { get; private set; }

        protected abstract void StartJob();

        protected virtual void OnTriggered()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;
            StartJob();
            JobStarted?.Invoke();
        }

        protected override void SubscribeOnEvents()
        {
            foreach (var trigger in _triggers)
            {
                trigger.Triggered += OnTriggered;
            }
        }

        protected override void UnsubscribeFromEvents()
        {
            foreach (var trigger in _triggers)
            {
                trigger.Triggered -= OnTriggered;
            }
        }

        protected void AddTriggers(List<IMechanismsTrigger> triggers)
        {
            _triggers.AddRange(triggers);

            if (CurrentInitializableMonoBehaviourStatus != InitializableMonoBehaviourStatus.Initialized ||
                !isActiveAndEnabled)
            {
                return;
            }

            foreach (var trigger in triggers)
            {
                trigger.Triggered += OnTriggered;
            }
        }

        protected void HandleDoneJob()
        {
            IsBusy = false;
            JobEnded?.Invoke();
        }
    }
}