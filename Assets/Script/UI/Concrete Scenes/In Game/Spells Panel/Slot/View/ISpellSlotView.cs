using UI.Concrete_Scenes.In_Game.Spells_Panel.Slot_Information;
using UnityEngine;

namespace UI.Concrete_Scenes.In_Game.Spells_Panel.Slot.View
{
    public interface ISpellSlotView
    {
        public void Appear(ISlotInformation slot, Sprite sprite);
        public void AppearAsEmptySlot(ISlotInformation slot);
        public void MoveToSlot(ISlotInformation slot);
        public void Disappear();
        public void ChangeBackgroundColor(Color newBackgroundColor);
    }
}