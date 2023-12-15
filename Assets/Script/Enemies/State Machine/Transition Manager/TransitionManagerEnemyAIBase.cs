using System;
using Common.Abstract_Bases.Disableable;
using Enemies.State_Machine.States;

namespace Enemies.State_Machine.Transition_Manager
{
    public abstract class TransitionManagerEnemyAIBase : BaseWithDisabling, ITransitionManagerEnemyAIWithDisabling
    {
        private bool _isActive;
        public abstract event Action<IStateEnemyAI> NeedTransit;

        public abstract bool TryTransit(out IStateEnemyAI stateEnemyAI);

        public void StartCheckingConditions()
        {
            _isActive = true;
            SubscribeOnTransitionEvents();
            HandleStartCheckingConditions();
        }

        public void StopCheckingConditions()
        {
            _isActive = false;
            UnsubscribeFromTransitionEvents();
            HandleStopCheckingConditions();
        }

        protected abstract void HandleStartCheckingConditions();
        protected abstract void HandleStopCheckingConditions();
        protected abstract void SubscribeOnTransitionEvents();
        protected abstract void UnsubscribeFromTransitionEvents();

        protected override void SubscribeOnEvents()
        {
            if (_isActive)
            {
                SubscribeOnTransitionEvents();
            }
        }

        protected override void UnsubscribeFromEvents()
        {
            UnsubscribeFromTransitionEvents();
        }
    }
}