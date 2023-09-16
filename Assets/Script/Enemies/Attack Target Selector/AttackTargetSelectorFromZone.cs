using System.Collections.Generic;
using Common.Abstract_Bases.Box_Collider_Trigger;
using UnityEngine;

namespace Enemies.Attack_Target_Selector
{
    [RequireComponent(typeof(BoxCollider))]
    public class AttackTargetSelectorFromZone : TriggerForInitializableObjectsBase<IEnemyTarget>,
        IAttackTargetSelectorFromZone
    {
        public IReadOnlyCollection<IEnemyTarget> GetTargetsInCollider()
        {
            return GetRequiredObjectsInCollider();
        }
    }
}