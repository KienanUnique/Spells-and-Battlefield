using UI.Spells_Panel.Slot_Information;
using UnityEngine;

namespace UI.Spells_Panel.Slot.View
{
    public interface ISpellSlotView
    {
        public void Appear(ISlotInformation slot, Sprite sprite);
        public void MoveToSlot(ISlotInformation slot);
        public void Disappear();
    }
}