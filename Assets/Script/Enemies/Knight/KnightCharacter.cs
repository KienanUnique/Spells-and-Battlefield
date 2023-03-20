using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Enemies.Knight
{
    public class KnightCharacter : Character
    {
        protected override string NamePrefix => "Knight";
        [SerializeField] private int _attackSwordDamage;

        public void DamageTargetsWithSwordAttack(List<ICharacter> targets)
        {
            targets.ForEach(target => target.HandleDamage(_attackSwordDamage));
        }
    }
}