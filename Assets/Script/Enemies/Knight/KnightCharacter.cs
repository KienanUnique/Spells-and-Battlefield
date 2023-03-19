using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Enemies.Knight
{
    public class KnightCharacter : Character
    {
        [SerializeField] private int _attackSwordDamage;

        public void DamageTargetsWithSwordAttack(List<ICharacter> targets)
        {
            targets.ForEach(target => target.HandleDamage(_attackSwordDamage));
        }

        public override void HandleHeal(int countOfHealPoints)
        {
            Debug.Log($"Enemy -> HandleHeal: {countOfHealPoints}");
        }

        public override void HandleDamage(int countOfHealPoints)
        {
            Debug.Log($"Enemy -> HandleDamage: {countOfHealPoints}");
        }
    }
}