using System.Collections.Generic;
using Common.Abstract_Bases;
using Interfaces;
using UnityEngine;

namespace Enemies.Attack_Target_Selector
{
    [RequireComponent(typeof(BoxCollider))]
    public class AttackTargetSelectorFromZone : BoxColliderTriggerBase<IEnemyTarget>, IAttackTargetSelectorFromZone
    {
        public IReadOnlyCollection<IEnemyTarget> GetTargetsInCollider() => GetRequiredObjectsInCollider();
    }
}