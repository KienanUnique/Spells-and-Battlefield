using Interfaces;
using Spells.Abstract_Types.Implementation_Bases.Implementations;
using Spells.Abstract_Types.Scriptable_Objects.Parts;
using Spells.Implementations_Interfaces.Implementations;
using UnityEngine;

namespace Spells.Concrete_Types.Mechanics
{
    [CreateAssetMenu(fileName = "Heal Mechanic",
        menuName = ScriptableObjectsMenuDirectories.SpellMechanicDirectory + "Heal Mechanic", order = 0)]
    public class HealMechanic : SpellMechanicEffectScriptableObject
    {
        [SerializeField] private int _healPoints;

        public override ISpellMechanicEffect GetImplementationObject() =>
            new HealInstantMechanicImplementation(_healPoints);

        private class HealInstantMechanicImplementation : SpellInstantMechanicEffectImplementationBase
        {
            private readonly int _healPoints;

            public HealInstantMechanicImplementation(int healPoints) => _healPoints = healPoints;

            public override void ApplyEffectToTarget(ISpellInteractable target)
            {
                if (target is IHealable healableTarget)
                {
                    healableTarget.HandleHeal(_healPoints);
                }
            }
        }
    }
}