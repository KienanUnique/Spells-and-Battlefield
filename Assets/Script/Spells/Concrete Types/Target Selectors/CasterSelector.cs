using System.Collections.Generic;
using Interfaces;
using Spells.Abstract_Types.Implementation_Bases.Implementations;
using Spells.Abstract_Types.Scriptable_Objects.Parts;
using Spells.Implementations_Interfaces.Implementations;
using UnityEngine;

namespace Spells.Concrete_Types.Target_Selectors
{
    [CreateAssetMenu(fileName = "Caster Selector",
        menuName = ScriptableObjectsMenuDirectories.SpellTargetSelectorDirectory + "Caster Selector", order = 0)]
    public class CasterSelector : SpellTargetSelectorScriptableObject
    {
        public override ISpellTargetSelector GetImplementationObject()
        {
            return new CasterSelectorImplementation();
        }

        private class CasterSelectorImplementation : SpellTargetSelectorImplementationBase
        {
            public override List<ISpellInteractable> SelectTargets()
            {
                var resultList = new List<ISpellInteractable>();
                if (Caster is ISpellInteractable casterSpellInteractable)
                {
                    resultList.Add(casterSpellInteractable);
                }

                return resultList;
            }
        }
    }
}