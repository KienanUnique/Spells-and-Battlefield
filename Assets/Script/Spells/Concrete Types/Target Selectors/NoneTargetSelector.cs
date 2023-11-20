using System.Collections.Generic;
using Common.Interfaces;
using Common.Readonly_Transform;
using Spells.Abstract_Types.Scriptable_Objects.Parts;
using Spells.Data_For_Spell_Implementation;
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
            public void Initialize(IDataForSpellImplementation data)
            {
                throw new System.NotImplementedException();
            }

            public void HandleSpellInteractableTriggerEnter(ISpellInteractable spellInteractable)
            {
            }

            public void HandleSpellInteractableTriggerExit(ISpellInteractable spellInteractable)
            {
            }

            public IReadOnlyList<ISpellInteractable> SelectTargets()
            {
                return new List<ISpellInteractable>();
            }
        }
    }
}