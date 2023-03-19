using Interfaces;
using Spells.Abstract_Types.Implementation_Bases;
using Spells.Abstract_Types.Scriptable_Objects;
using Spells.Implementations_Interfaces;
using UnityEngine;

namespace Spells.Concrete_Types.Mechanics
{
    [CreateAssetMenu(fileName = "Damage Mechanic",
        menuName = "Spells and Battlefield/Spell System/Mechanic/Damage Mechanic", order = 0)]
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

            protected override void ApplyEffectToTarget(ISpellInteractable target)
            {
                target.HandleDamage(_damage);
            }
        }
    }
}