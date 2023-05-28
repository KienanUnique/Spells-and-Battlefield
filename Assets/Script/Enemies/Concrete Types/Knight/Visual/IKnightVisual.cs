using System;
using Enemies.Visual;

namespace Enemies.Concrete_Types.Knight.Visual
{
    public interface IKnightVisual : IEnemyVisualBase
    {
        event Action AttackWithSwordAnimationMomentStart;
        void StartAttackWithSwordAnimation();
        void StopAttackWithSwordAnimation();
    }
}