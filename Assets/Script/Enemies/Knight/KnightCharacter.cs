using System.Collections.Generic;
using UnityEngine;
public class KnightCharacter : Character
{
    [SerializeField] private int _attackSwordDamage;

    public void DamageTargetsWithSwordAttack(List<ICharacter> targets)
    {
        targets.ForEach(target => target.HandleDamage(_attackSwordDamage));
    }
}