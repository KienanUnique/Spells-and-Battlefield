using System.Collections.Generic;
using Interfaces;
using Spells.Abstract_Types.Scriptable_Objects;
using Spells.Implementations_Interfaces;
using UnityEngine;

namespace Spells.Concrete_Types.Mechanics
{
    [CreateAssetMenu(fileName = "None Mechanic",
        menuName = "Spells and Battlefield/Spell System/Mechanic/None Mechanic", order = 0)]
    public class NoneMechanic : SpellMechanicEffectScriptableObject
    {
        public override ISpellMechanicEffect GetImplementationObject() => new NoneMechanicImplementation();

        private class NoneMechanicImplementation : ISpellMechanicEffect
        {
            public void ApplyEffectToTargets(List<ISpellInteractable> targets)
            {
            }

            public void Initialize(Rigidbody spellRigidbody, Transform fromCastObjectTransform,
                ISpellInteractable casterCharacter)
            {
            }
        }
    }
}