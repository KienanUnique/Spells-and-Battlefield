using System.Collections.Generic;
using Spells.Abstract_Types.Implementation_Bases.Implementations;
using Spells.Abstract_Types.Scriptable_Objects.Parts;
using Spells.Implementations_Interfaces.Implementations;
using UnityEngine;

namespace Spells.Concrete_Types.Target_Selectors
{
    [CreateAssetMenu(fileName = "Targets In Collider Selector",
        menuName = ScriptableObjectsMenuDirectories.SpellTargetSelectorDirectory + "Targets In Collider Selector",
        order = 0)]
    public class TargetsInColliderSelector : SpellTargetSelectorScriptableObject
    {
        public override ISpellTargetSelector GetImplementationObject()
        {
            return new TargetsInColliderSelectorImplementation();
        }

        private class TargetsInColliderSelectorImplementation : SpellTargetSelectorImplementationBase
        {
            public override IReadOnlyList<ISpellInteractable> SelectTargets()
            {
                return _targetsDetector.TargetsInCollider;
            }
        }
    }
}