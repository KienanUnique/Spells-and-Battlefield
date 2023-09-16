using System.Collections.Generic;

namespace Enemies.Attack_Target_Selector
{
    public interface IAttackTargetSelectorFromZone
    {
        public IReadOnlyCollection<IEnemyTarget> GetTargetsInCollider();
    }
}