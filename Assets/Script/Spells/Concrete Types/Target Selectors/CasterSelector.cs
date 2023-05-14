using System.Collections.Generic;
using Interfaces;
using Spells.Abstract_Types.Implementation_Bases;
using Spells.Abstract_Types.Implementation_Bases.Implementations;
using Spells.Abstract_Types.Scriptable_Objects;
using Spells.Abstract_Types.Scriptable_Objects.Parts;
using Spells.Implementations_Interfaces;
using Spells.Implementations_Interfaces.Implementations;
using UnityEngine;

namespace Spells.Concrete_Types.Target_Selectors
{
    [CreateAssetMenu(fileName = "Caster Selector",
        menuName = "Spells and Battlefield/Spell System/Target Selector/Caster Selector", order = 0)]
    public class CasterSelector : SpellTargetSelectorScriptableObject
    {
        public override ISpellTargetSelector GetImplementationObject() => new CasterSelectorImplementation();

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