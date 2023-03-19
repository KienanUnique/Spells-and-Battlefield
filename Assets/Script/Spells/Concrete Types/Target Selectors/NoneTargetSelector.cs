using System.Collections.Generic;
using Interfaces;
using Spells.Abstract_Types.Scriptable_Objects;
using Spells.Implementations_Interfaces;
using UnityEngine;

namespace Spells.Concrete_Types.Target_Selectors
{
    [CreateAssetMenu(fileName = "None Target Selector",
        menuName = "Spells and Battlefield/Spell System/Target Selector/None Target Selector", order = 0)]
    public class NoneTargetSelector : SpellTargetSelecterScriptableObject
    {
        public override ISpellTargetSelector GetImplementationObject() => new NoneSelectorImplementation();

        private class NoneSelectorImplementation : ISpellTargetSelector
        {
            public void Initialize(Rigidbody spellRigidbody, Transform fromCastObjectTransform,
                ISpellInteractable casterCharacter)
            {
            }

            public List<ISpellInteractable> SelectTargets() => new List<ISpellInteractable>();
        }
    }
}