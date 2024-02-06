using System;
using System.Collections.Generic;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Puzzles.Mechanisms_Triggers;

namespace Puzzles.Mechanisms
{
    public abstract class MechanismControllerBase : InitializableMonoBehaviourBase, IMechanismController
    {
        private readonly List<IMechanismsTrigger> _triggers = new List<IMechanismsTrigger>();

        public event Action JobStarted;
        public event Action JobEnded;

        protected bool IsBusy { get; private set; }

        protected abstract void StartJob();

        protected void AddTriggers(List<IMechanismsTrigger> triggers)
        {
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
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;
            StartJob();
            JobStarted?.Invoke();
        }

        protected void HandleDoneJob()
        {
            IsBusy = false;
            JobEnded?.Invoke();
        }
    }
}