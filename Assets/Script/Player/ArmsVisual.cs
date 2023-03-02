using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ArmsVisual : MonoBehaviour
{
    public Action UseSpellAnimationMomentStart;
    private Animator _handsAnimator;
    private static readonly int AttackTriggerHash = Animator.StringToHash("Attack");

    public void InvokeUseSpellAnimationMomentStart() => UseSpellAnimationMomentStart?.Invoke();

    public void PlayUseSpellAnimation(AnimatorOverrideController _useSpellHandsAnimatorController)
    {
        _handsAnimator.runtimeAnimatorController = _useSpellHandsAnimatorController;
        _handsAnimator.SetTrigger(AttackTriggerHash);
    }

    private void Awake() {
        _handsAnimator = GetComponent<Animator>();
    }
}