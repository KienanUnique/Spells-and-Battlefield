using UnityEngine;

[CreateAssetMenu(fileName = "Damage Mechanic", menuName = "Spells and Battlefield/Spell System/Mechanic/Damage Mechanic", order = 0)]
public class DamageMechanic : SpellMechanicEffectScriptableObject
{
    [SerializeField] private int _damage;
    protected override void ApplyEffectToTarget(ICharacter target)
    {
        target.HandleDamage(_damage);
    }
}