using System.Collections.Generic;
using Common.Abstract_Bases;
using Common.Abstract_Bases.Box_Collider_Trigger;
using Interfaces;
using UnityEngine;

namespace Enemies.Attack_Target_Selector
{
    [RequireComponent(typeof(BoxCollider))]
    public class AttackTargetSelectorFromZone : BoxColliderTriggerBase<IEnemyTarget>, IAttackTargetSelectorFromZone
    {
        public IReadOnlyCollection<IEnemyTarget> GetTargetsInCollider()
        {
            return GetRequiredObjectsInCollider();
        }
    }
}