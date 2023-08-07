using System;
using Common.Abstract_Bases.Disableable;
using Enemies.State_Machine.States;

namespace Enemies.State_Machine.Transition_Manager
{
    public abstract class TransitionManagerEnemyAIBase : BaseWithDisabling, ITransitionManagerEnemyAIWithDisabling
    {
        public abstract event Action<IStateEnemyAI> NeedTransit;
        private bool _isActive;

        public void StartCheckingConditions()
        {
            _isActive = true;
            HandleStartCheckingConditions();
            SubscribeOnTransitionEvents();
        }

        public void StopCheckingConditions()
        {
            _isActive = false;
            HandleStopCheckingConditions();
            UnsubscribeFromTransitionEvents();
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