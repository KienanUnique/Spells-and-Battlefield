using System;
using System.Collections;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Enemies.State_Machine.States;
using UnityEngine;

namespace Enemies.State_Machine.Transitions
{
    public abstract class TransitionEnemyAI : InitializableMonoBehaviourBase, ITransitionEnemyAI, IInitializableTransitionEnemyAI
    {
        [SerializeField] private StateEnemyAI _targetStateEnemyAI;
        private Coroutine _currentCheckConditionsCoroutine;

        public void Initialize(IEnemyStateMachineControllable stateMachineControllable)
        {
            StateMachineControllable = stateMachineControllable;
            SetInitializedStatus();
        }
        public event Action<IStateEnemyAI> NeedTransit;

        protected IEnemyStateMachineControllable StateMachineControllable { get; private set; }

        public void StartCheckingConditions()
        {
            if (_currentCheckConditionsCoroutine != null) return;
            
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