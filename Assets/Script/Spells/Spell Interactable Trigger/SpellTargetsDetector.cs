using System.Collections.Generic;
using Common.Abstract_Bases.Box_Collider_Trigger;

namespace Spells.Spell_Interactable_Trigger
{
    public class SpellTargetsDetector : ColliderTriggerBase<ISpellInteractable>, ISpellTargetsDetector
    {
        public IReadOnlyList<ISpellInteractable> TargetsInCollider => _requiredObjectsInside;
    }
}