using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerVisual : MonoBehaviour
{
    public Action UseSpellAnimationMomentStart;
    private Animator _characterAnimator;
    private static readonly int _attackTriggerHash = Animator.StringToHash("Attack");
    private static readonly int _isRunningBoolHash = Animator.StringToHash("IsRunning");

    public void InvokeUseSpellAnimationMomentStart() => UseSpellAnimationMomentStart?.Invoke();

    public void PlayUseSpellAnimation(AnimatorOverrideController _useSpellHandsAnimatorController)
    {
        _characterAnimator.runtimeAnimatorController = _useSpellHandsAnimatorController;
        _characterAnimator.SetTrigger(_attackTriggerHash);
    }

    public void HandlePlayerMovingStatusChange(MovingStatusEnum movingStatusEnum)
    {
        switch (movingStatusEnum)
        {
            case MovingStatusEnum.Idle:
                StartIdleAnimation();
                break;
            case MovingStatusEnum.Running:
                StartRunningAnimation();
                break;
        }
    }
    public void StartRunningAnimation()
    {
        _characterAnimator.SetBool(_isRunningBoolHash, true);
    }

    public void StartIdleAnimation()
    {
        _characterAnimator.SetBool(_isRunningBoolHash, false);
    }

    private void Awake()
    {
        _characterAnimator = GetComponent<Animator>();
    }
}