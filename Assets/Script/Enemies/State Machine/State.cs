using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces;
using UnityEngine;

namespace Enemies.State_Machine
{
    public abstract class State : MonoBehaviour
    {
        [SerializeField] private List<Transition> _transitions;
        private bool _isAlreadyActivated = false;
        protected IPlayer Target { get; private set; }

        public virtual void Enter(IPlayer target)
        {
            if (_isAlreadyActivated)
            {
                throw new StateIsAlreadyActivatedException();
            }

            Target = target;
            _isAlreadyActivated = true;
            foreach (var transition in _transitions)
            {
                transition.StartCheckingConditions(Target);
            }
        }

        public virtual void Exit()
        {
            if (!_isAlreadyActivated)
            {
                throw new TryingDeactivateNotActivatedStateException();
            }

            foreach (var transition in _transitions)
            {
                transition.StopCheckingConditions();
            }

            _isAlreadyActivated = false;
        }

        public bool NeedToSwitchToNextState(out State nextState)
        {
            foreach (var transition in _transitions.Where(transition => transition.NeedTransit))
            {
                nextState = transition.TargetState;
                return true;
            }

            nextState = null;
            return false;
        }

        private class StateIsAlreadyActivatedException : Exception
        {
            public StateIsAlreadyActivatedException() : base("State is already activated")
            {
            }
        }

        private class TryingDeactivateNotActivatedStateException : Exception
        {
            public TryingDeactivateNotActivatedStateException() : base("Can't deactivate not active state")
            {
            }
        }
    }
}