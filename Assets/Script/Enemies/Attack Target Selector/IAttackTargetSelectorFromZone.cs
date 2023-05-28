using System.Collections.Generic;
using Interfaces;

namespace Enemies.Attack_Target_Selector
{
    public interface IAttackTargetSelectorFromZone
    {
        public IReadOnlyCollection<IEnemyTarget> GetTargetsInCollider();
    }
}