using System.Collections.Generic;

namespace Spells.Spell_Interactable_Trigger
{
    public interface ISpellTargetsDetector
    {
        public IReadOnlyList<ISpellInteractable> TargetsInCollider { get; }
    }
}