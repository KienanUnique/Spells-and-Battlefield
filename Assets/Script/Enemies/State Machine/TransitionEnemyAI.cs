using System;
using System.Collections;
using UnityEngine;

namespace Enemies.State_Machine
{
    public abstract class TransitionEnemyAI : MonoBehaviour
    {
        [SerializeField] private StateEnemyAI _targetStateEnemyAI;
        private Coroutine _currentCheckConditionsCoroutine;

        public event Action<StateEnemyAI> NeedTransit;

        protected IEnemyStateMachineControllable StateMachineControllable { get; private set; }

        public void StartCheckingConditions(IEnemyStateMachineControllable stateMachineControllable)
        {
            if (_currentCheckConditionsCoroutine != null) return;

            StateMachineControllable = stateMachineControllable;
            _currentCheckConditionsCoroutine = StartCoroutine(CheckConditionsCoroutine());
        }

        public void StopCheckingConditions()
        {
            if (_currentCheckConditionsCoroutine == null) return;
            StopCoroutine(_currentCheckConditionsCoroutine);

            _currentCheckConditionsCoroutine = null;
        }

        protected virtual void SpecialActionOnStartChecking()
        {
        }

        protected abstract void CheckConditions();

        protected void InvokeTransitionEvent()
        {
            NeedTransit?.Invoke(_targetStateEnemyAI);
        }

        private IEnumerator CheckConditionsCoroutine()
        {
            while (true)
            {
                CheckConditions();
                yield return null;
            }
        }
    }
}