using Spells.Spell;
using UI.Spells_Panel.Slot_Information;

namespace UI.Spells_Panel.Slot_Controller
{
    public interface ISpellSlot
    {
        public ISlotInformation CurrentSlotInformation { get; }
        public ISpell CurrentSpell { get; }
        public void MoveToSlot(ISlotInformation slot);
        public void AppearAsSlot(ISlotInformation slot, ISpell spellToRepresent);
        public void AppearAsEmptySlot(ISlotInformation slot);
        public void DisappearAndForgetSpell();
    }
}