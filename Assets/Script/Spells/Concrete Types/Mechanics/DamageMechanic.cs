using UnityEngine;

[CreateAssetMenu(fileName = "Damage Mechanic", menuName = "Spells and Battlefield/Spell System/Mechanic/Damage Mechanic", order = 0)]
public class DamageMechanic : SpellMechanicEffectScriptableObject
{
    [SerializeField] private int _damage;

    public override ISpellMechanicEffect GetImplementationObject()
    {
        return new DamageMechanicImplementation(_damage);
    }

    private class DamageMechanicImplementation : SpellMechanicEffectImplementationBase
    {
        private int _damage;

        public DamageMechanicImplementation(int damage) => _damage = damage;

        protected override void ApplyEffectToTarget(ICharacter target)
        {
            target.HandleDamage(_damage);
        }
    }
}