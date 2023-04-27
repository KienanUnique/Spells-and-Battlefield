using System;
using UnityEngine;

namespace Enemies.Knight
{
    public class KnightVisual : EnemyVisualBase
    {
        private static readonly int IsAttackingBoolHash = Animator.StringToHash("Is Attacking");
        public event Action AttackWithSwordAnimationMomentStart;

        public void InvokeAttackWithSwordAnimationMomentStart() => AttackWithSwordAnimationMomentStart?.Invoke();

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