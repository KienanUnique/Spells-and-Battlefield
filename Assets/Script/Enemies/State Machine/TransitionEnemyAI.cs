using System;
using System.Collections;
using UnityEngine;

namespace Enemies.State_Machine
{
    public abstract class TransitionEnemyAI : MonoBehaviour
    {
        public event Action<StateEnemyAI> NeedTransit;
        public StateEnemyAI TargetStateEnemyAI => _targetStateEnemyAI;
        [SerializeField] private StateEnemyAI _targetStateEnemyAI;
        protected IEnemyStateMachineControllable StateMachineControllable { get; private set; }
        protected Coroutine _currentCheckConditionsCoroutine = null;

        public void StartCheckingConditions(IEnemyStateMachineControllable stateMachineControllable)
        {
            if (_currentCheckConditionsCoroutine != null)
            {
                throw new TransitionIsAlreadyActivatedException();
            }

            StateMachineControllable = stateMachineControllable;
            _currentCheckConditionsCoroutine = StartCoroutine(CheckConditionsCoroutine());
        }

        public void StopCheckingConditions()
        {
            if (_currentCheckConditionsCoroutine == null)
            {
                throw new TryingDeactivateNotActivatedTransitionException();
            }

            StopCoroutine(_currentCheckConditionsCoroutine);
            _currentCheckConditionsCoroutine = null;
        }

        protected virtual void SpecialActionOnStartChecking()
        {
        }

        protected abstract void CheckConditions();

        protected void InvokeTransitionEvent()
        {
            NeedTransit?.Invoke(TargetStateEnemyAI);
        }

        private IEnumerator CheckConditionsCoroutine()
        {
            while (true)
            {
                CheckConditions();
                yield return null;
            }
        }

        private class TransitionIsAlreadyActivatedException : Exception
        {
            public TransitionIsAlreadyActivatedException() : base("Transition is already activated")
            {
            }
        }

        private class TryingDeactivateNotActivatedTransitionException : Exception
        {
            public TryingDeactivateNotActivatedTransitionException() : base("Can't deactivate not active transition")
            {
            }
        }
    }
}