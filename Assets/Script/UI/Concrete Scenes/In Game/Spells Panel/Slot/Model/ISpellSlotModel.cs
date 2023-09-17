using Spells.Spell;
using UI.Concrete_Scenes.In_Game.Spells_Panel.Slot_Information;

namespace UI.Concrete_Scenes.In_Game.Spells_Panel.Slot.Model
{
    public interface ISpellSlotModel
    {
        public ISpell CurrentSpell { get; }
        public ISlotInformation CurrentSlotInformation { get; }
        public bool IsVisible { get; }
        public bool IsEmptySlot { get; }
        public void Appear(ISpell spell, ISlotInformation slot, bool isShowingEmptySlot);
        public void Disappear();
        public void MoveToSlot(ISlotInformation slot);
    }
}