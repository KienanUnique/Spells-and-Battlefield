using Interfaces;
using Spells.Abstract_Types.Implementation_Bases;
using Spells.Abstract_Types.Scriptable_Objects;
using Spells.Implementations_Interfaces;
using UnityEngine;

namespace Spells.Concrete_Types.Mechanics
{
    [CreateAssetMenu(fileName = "Heal Mechanic",
        menuName = "Spells and Battlefield/Spell System/Mechanic/Heal Mechanic", order = 0)]
    public class HealMechanic : SpellMechanicEffectScriptableObject
    {
        [SerializeField] private int _healPoints;

        public override ISpellMechanicEffect GetImplementationObject() => new HealInstantMechanicImplementation(_healPoints);

        private class HealInstantMechanicImplementation : SpellInstantMechanicEffectImplementationBase
        {
            private int _healPoints;

            public HealInstantMechanicImplementation(int healPoints) => _healPoints = healPoints;

            public override void ApplyEffectToTarget(ISpellInteractable target)
            {
                target.HandleHeal(_healPoints);
            }
        }
    }
}