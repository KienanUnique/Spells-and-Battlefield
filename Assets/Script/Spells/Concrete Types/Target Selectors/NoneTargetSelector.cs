using System.Collections.Generic;
using Spells.Abstract_Types.Scriptable_Objects.Parts;
using Spells.Implementations_Interfaces.Implementations;
using UnityEngine;

namespace Spells.Concrete_Types.Target_Selectors
{
    [CreateAssetMenu(fileName = "None Target Selector",
        menuName = ScriptableObjectsMenuDirectories.SpellTargetSelectorDirectory + "None Target Selector", order = 0)]
    public class NoneTargetSelector : SpellTargetSelectorScriptableObject
    {
        public override ISpellTargetSelector GetImplementationObject()
        {
            return new NoneSelectorImplementation();
        }

        private class NoneSelectorImplementation : ISpellTargetSelector
        {
            public void Initialize(Rigidbody spellRigidbody, ICaster caster)
            {
            }

            public List<ISpellInteractable> SelectTargets()
            {
                return new List<ISpellInteractable>();
            }
        }
    }
}