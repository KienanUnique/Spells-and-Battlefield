using Interfaces;
using Spells.Abstract_Types.Implementation_Bases;
using Spells.Abstract_Types.Implementation_Bases.Implementations;
using Spells.Abstract_Types.Scriptable_Objects;
using Spells.Abstract_Types.Scriptable_Objects.Parts;
using Spells.Implementations_Interfaces;
using Spells.Implementations_Interfaces.Implementations;
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
            return new DamageInstantMechanicImplementation(_damage);
        }

        private class DamageInstantMechanicImplementation : SpellInstantMechanicEffectImplementationBase
        {
            private readonly int _damage;

            public DamageInstantMechanicImplementation(int damage) => _damage = damage;

            public override void ApplyEffectToTarget(ISpellInteractable target)
            {
                target.HandleDamage(_damage);
            }
        }
    }
}