using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    [SerializeField] private List<Transition> _transitions;
    private bool IsAlreadyActivated = false;
    protected IPlayer _target { get; set; }

    public virtual void Enter(IPlayer target)
    {
        if (IsAlreadyActivated)
        {
            throw new StateIsAlreadyActivatedException();
        }

        _target = target;
        IsAlreadyActivated = true;
        foreach (var transition in _transitions)
        {
            transition.StartCheckingConditions(_target);
        }
    }

    public virtual void Exit()
    {
        if (!IsAlreadyActivated)
        {
            throw new DisactivateNotActivatedStateException();
        }

        foreach (var transition in _transitions)
        {
            transition.StopCheckingConditions();
        }
        IsAlreadyActivated = false;
    }

    public bool NeedToSwitchToNextState(out State nextState)
    {
        foreach (var transition in _transitions)
        {
            if (transition.NeedTransit)
            {
                nextState = transition.TargetState;
                return true;
            }
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

    private class DisactivateNotActivatedStateException : Exception
    {
        public DisactivateNotActivatedStateException() : base("Can't disactivate not active state")
        {
        }
    }
}
