using System;
using Spells.Spell;
using UI.Spells_Panel.Slot_Information;

namespace UI.Spells_Panel.Slot.Model
{
    public class SpellSlotModel : ISpellSlotModel
    {
        public SpellSlotModel(ISlotInformation currentSlotInformation)
        {
            IsVisible = false;
            CurrentSpell = null;
            CurrentSlotInformation = currentSlotInformation;
            IsEmptySlot = true;
        }

        public ISpell CurrentSpell { get; private set; }

        public ISlotInformation CurrentSlotInformation { get; private set; }

        public bool IsVisible { get; private set; }

        public bool IsEmptySlot { get; private set; }

        public void Appear(ISpell spell, ISlotInformation slot, bool isShowingEmptySlot)
        {
            if (IsVisible)
            {
                throw new InvalidOperationException("Slot is already visible");
            }

            IsVisible = true;
            CurrentSpell = spell;
            CurrentSlotInformation = slot;
            IsEmptySlot = isShowingEmptySlot;
        }

        public void Disappear()
        {
            if (!IsVisible)
            {
                throw new InvalidOperationException("Slot is already non visible");
            }

            IsVisible = false;
            CurrentSpell = null;
            CurrentSlotInformation = null;
            IsEmptySlot = true;
        }

        public void MoveToSlot(ISlotInformation slot)
        {
            if (!IsVisible)
            {
                throw new InvalidOperationException("Unable to move non visible slot");
            }

            CurrentSlotInformation = slot;
        }
    }
}