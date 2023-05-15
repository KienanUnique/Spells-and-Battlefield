using System.Collections.Generic;
using Interfaces;
using Spells.Abstract_Types.Scriptable_Objects.Parts;
using Spells.Implementations_Interfaces.Implementations;
using UnityEngine;

namespace Spells.Concrete_Types.Mechanics
{
    [CreateAssetMenu(fileName = "None Mechanic",
        menuName = ScriptableObjectsMenuDirectories.SpellMechanicDirectory + "None Mechanic", order = 0)]
    public class NoneMechanic : SpellMechanicEffectScriptableObject
    {
        public override ISpellMechanicEffect GetImplementationObject() => new NoneMechanicImplementation();

        private class NoneMechanicImplementation : ISpellMechanicEffect
        {
            public void Initialize(Rigidbody spellRigidbody, ICaster caster)
            {
            }

            public void ApplyEffectToTargets(List<ISpellInteractable> targets)
            {
            }

            public void ApplyEffectToTarget(ISpellInteractable target)
            {
            }
        }
    }
}