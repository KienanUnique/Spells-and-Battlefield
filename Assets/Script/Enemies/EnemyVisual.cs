using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyVisual : MonoBehaviour
{
    public Action AttackAnimationMomentStartEvent;
    private Animator _characterAnimator;
    private static readonly int _attackTriggerHash = Animator.StringToHash("Attack");
    private static readonly int _isRunningBoolHash = Animator.StringToHash("Is Running");

    public void InvokeAttackAnimationMomentStart() => AttackAnimationMomentStartEvent?.Invoke();

    public void PlayAttackAnimation()
    {
        _characterAnimator.SetTrigger(_attackTriggerHash);
    }

    public void UpdateMovingData(bool isRunning)
    {
        _characterAnimator.SetBool(_isRunningBoolHash, isRunning);
    }

    private void Awake()
    {
        _characterAnimator = GetComponent<Animator>();
    }
}
