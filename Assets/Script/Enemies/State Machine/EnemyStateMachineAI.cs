using System.Collections;
using Enemies.State_Machine;
using Interfaces;
using UnityEngine;

public class EnemyStateMachineAI : MonoBehaviour
{
    public State CurrentState { private set; get; }
    [SerializeField] private State _firstState;
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
