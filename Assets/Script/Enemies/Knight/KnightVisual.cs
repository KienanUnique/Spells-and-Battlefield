using System;
using UnityEngine;

namespace Enemies.Knight
{
    public class KnightVisual : EnemyVisualBase
    {
        public Action AttackWithSwordAnimationMomentStartEvent;
        public void InvokeAttackWithSwordAnimationMomentStart() => AttackWithSwordAnimationMomentStartEvent?.Invoke();
        private static readonly int IsAttackingBoolHash = Animator.StringToHash("Is Attacking");

        public void StartAttackWithSwordAnimation()
        {
            _characterAnimator.SetBool(IsAttackingBoolHash, true);
        }

        public void StopAttackWithSwordAnimation()
        {
            _characterAnimator.SetBool(IsAttackingBoolHash, false);
        }
    }
}