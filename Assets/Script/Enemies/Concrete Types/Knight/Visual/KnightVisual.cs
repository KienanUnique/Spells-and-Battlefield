using System;
using Enemies.Visual;
using UnityEngine;

namespace Enemies.Concrete_Types.Knight.Visual
{
    public class KnightVisual : EnemyVisualBase, IKnightVisual
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