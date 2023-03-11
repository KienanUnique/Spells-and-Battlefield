using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerVisual : MonoBehaviour
{
    public Action UseSpellAnimationMomentStartEvent;
    private Animator _characterAnimator;
    private static readonly int _attackTriggerHash = Animator.StringToHash("Attack");
    private static readonly int _movingDirectionXFloatHash = Animator.StringToHash("Moving Direction X");
    private static readonly int _movingDirectionYFloatHash = Animator.StringToHash("Moving Direction Y");
    private static readonly int _ratioOfCurrentVelocityToMaximumVelocityFloatHash = Animator.StringToHash("Ratio Of Current Velocity To Maximum Velocity");
    private static readonly int _jumpTriggerHash = Animator.StringToHash("Jump");
    private static readonly int _fallTriggerHash = Animator.StringToHash("Fall");
    private static readonly int _landTriggerHash = Animator.StringToHash("Land");

    public void InvokeUseSpellAnimationMomentStart() => UseSpellAnimationMomentStartEvent?.Invoke();

    public void PlayUseSpellAnimation(AnimatorOverrideController _useSpellHandsAnimatorController)
    {
        _characterAnimator.runtimeAnimatorController = _useSpellHandsAnimatorController;
        _characterAnimator.SetTrigger(_attackTriggerHash);
    }

    public void PlayJumpAnimation(){
        _characterAnimator.SetTrigger(_jumpTriggerHash);
        _characterAnimator.ResetTrigger(_fallTriggerHash);
        _characterAnimator.ResetTrigger(_landTriggerHash);
    }

    public void PlayFallAnimation(){
        _characterAnimator.ResetTrigger(_jumpTriggerHash);
        _characterAnimator.SetTrigger(_fallTriggerHash);
        _characterAnimator.ResetTrigger(_landTriggerHash);
    }

    public void PlayLandAnimation(){
        _characterAnimator.ResetTrigger(_jumpTriggerHash);
        _characterAnimator.ResetTrigger(_fallTriggerHash);
        _characterAnimator.SetTrigger(_landTriggerHash);
    }

    public void UpdateMovingData(Vector2 movingDirectionNormalized, float ratioOfCurrentVelocityToMaximumVelocity)
    {
        _characterAnimator.SetFloat(_movingDirectionXFloatHash, movingDirectionNormalized.x);
        _characterAnimator.SetFloat(_movingDirectionYFloatHash, movingDirectionNormalized.y);
        _characterAnimator.SetFloat(_ratioOfCurrentVelocityToMaximumVelocityFloatHash, ratioOfCurrentVelocityToMaximumVelocity);
    }

    private void Awake()
    {
        _characterAnimator = GetComponent<Animator>();
    }
}