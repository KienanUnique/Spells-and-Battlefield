using System.Collections;
using Interfaces;
using UnityEngine;

namespace Enemies.State_Machine
{
    public class EnemyStateMachineAI : MonoBehaviour
    {
        [SerializeField] private State _firstState;
        private State CurrentState { set; get; }
        private IPlayer _target;
        private Coroutine _currentUpdateCoroutine = null;

        public void StartStateMachine(IPlayer target)
        {
            _target = target;
            if (_currentUpdateCoroutine == null)
            {
                TransitToState(_firstState);
                _currentUpdateCoroutine = StartCoroutine(UpdateStateMachine());
            }
        }

        public void StopStateMachine()
        {
            if (_currentUpdateCoroutine != null)
            {
                StopCoroutine(_currentUpdateCoroutine);
                TransitToState(null);
                _currentUpdateCoroutine = null;
            }
        }

        private IEnumerator UpdateStateMachine()
        {
            while (true)
            {
                if (CurrentState != null && CurrentState.NeedToSwitchToNextState(out State nextState))
                {
                    TransitToState(nextState);
                }

                yield return null;
            }
        }

        private void TransitToState(State nextState)
        {
            if (CurrentState != null)
            {
                CurrentState.Exit();
            }

            CurrentState = nextState;

            if (CurrentState != null)
            {
                CurrentState.Enter(_target);
            }
        }
    }
}