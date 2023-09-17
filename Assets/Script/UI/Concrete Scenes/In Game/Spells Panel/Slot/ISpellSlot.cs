using Spells.Spell;
using UI.Concrete_Scenes.In_Game.Spells_Panel.Slot_Information;
using UnityEngine;

namespace UI.Concrete_Scenes.In_Game.Spells_Panel.Slot
{
    public interface ISpellSlot
    {
        public bool IsEmptySlot { get; }
        public ISlotInformation CurrentSlotInformation { get; }
        public ISpell CurrentSpell { get; }
        public void MoveToSlot(ISlotInformation slot);
        public void AppearAsSlot(ISlotInformation slot, ISpell spellToRepresent);
        public void AppearAsEmptySlot(ISlotInformation slot);
        public void DisappearAndForgetSpell();
        public void ChangeBackgroundColor(Color newBackgroundColor);
    }
}