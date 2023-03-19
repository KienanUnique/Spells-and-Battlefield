using System.Collections.Generic;
using Interfaces;
using Spells.Abstract_Types.Implementation_Bases;
using Spells.Abstract_Types.Scriptable_Objects;
using Spells.Implementations_Interfaces;
using UnityEngine;

namespace Spells.Concrete_Types.Target_Selectors
{
    [CreateAssetMenu(fileName = "Caster Selector",
        menuName = "Spells and Battlefield/Spell System/Target Selector/Caster Selector", order = 0)]
    public class CasterSelector : SpellTargetSelecterScriptableObject
    {
        public override ISpellTargetSelector GetImplementationObject() => new CasterSelectorImplementation();

        private class CasterSelectorImplementation : SpellTargetSelectorImplementationBase
        {
            public override List<ISpellInteractable> SelectTargets() =>
                new List<ISpellInteractable>() {_casterInterface};
        }
    }
}