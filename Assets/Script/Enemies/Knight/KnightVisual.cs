using System;
using UnityEngine;

public class KnightVisual : EnemyVisualBase
{
    public Action AttackWithSwordAnimationMomentStartEvent;
    public void InvokeAttackWithSwordAnimationMomentStart() => AttackWithSwordAnimationMomentStartEvent?.Invoke();
    private static readonly int _isAttackingBoolHash = Animator.StringToHash("Is Attacking");

    public void StartAttackWithSwordAnimation()
    {
        _characterAnimator.SetBool(_isAttackingBoolHash, true);
    }

    public void StopAttackWithSwordAnimation()
    {
        _characterAnimator.SetBool(_isAttackingBoolHash, false);
    }
}