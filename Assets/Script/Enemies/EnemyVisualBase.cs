using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class EnemyVisualBase : MonoBehaviour
{
    protected Animator _characterAnimator;
    private static readonly int _isRunningBoolHash = Animator.StringToHash("Is Running");

    public void UpdateMovingData(bool isRunning)
    {
        _characterAnimator.SetBool(_isRunningBoolHash, isRunning);
    }

    protected virtual void Awake()
    {
        _characterAnimator = GetComponent<Animator>();
    }
}
