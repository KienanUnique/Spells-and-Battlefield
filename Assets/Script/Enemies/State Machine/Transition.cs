﻿using System;
using System.Collections;
using UnityEngine;

public abstract class Transition : MonoBehaviour
{
    public bool NeedTransit { get; protected set; }
    public State TargetState => _targetState;
    [SerializeField] private State _targetState;
    protected IPlayer Target { get; private set; }
    protected Coroutine _currentCheckConditionsCoroutine = null;
    public void StartCheckingConditions(IPlayer target)
    {
        if (_currentCheckConditionsCoroutine != null)
        {
            throw new TransitionIsAlreadyActivatedException();
        }
        Target = target;
        NeedTransit = false;
        _currentCheckConditionsCoroutine = StartCoroutine(CheckConditionsCoroutine());
    }

    public void StopCheckingConditions()
    {
        if (_currentCheckConditionsCoroutine == null)
        {
            throw new DisactivateNotActivatedTransitionException();
        }
        StopCoroutine(_currentCheckConditionsCoroutine);
        _currentCheckConditionsCoroutine = null;
    }

    protected abstract void CheckConditions();

    protected virtual IEnumerator CheckConditionsCoroutine()
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

    private class DisactivateNotActivatedTransitionException : Exception
    {
        public DisactivateNotActivatedTransitionException() : base("Can't disactivate not active transition")
        {
        }
    }
}
