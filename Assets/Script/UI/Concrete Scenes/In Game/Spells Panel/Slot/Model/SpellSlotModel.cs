using System;
using Spells.Spell;
using UI.Concrete_Scenes.In_Game.Spells_Panel.Slot_Information;
using UnityEngine;

namespace UI.Concrete_Scenes.In_Game.Spells_Panel.Slot.Model
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
            if (isShowingEmptySlot)
            {
                Debug.Log($"Appear empty: {slot.LocalPosition}");
            }
            else
            {
                Debug.Log($"Appear: {spell.CardInformation.Title}, {slot.LocalPosition}");
            }
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
            Debug.Log(
                $"Disappear: {CurrentSpell.CardInformation.Title}, {CurrentSlotInformation.LocalPosition}, {IsEmptySlot}");
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
            Debug.Log(
                $"MoveToSlot: {CurrentSpell.CardInformation.Title}, {CurrentSlotInformation.LocalPosition} => {slot.LocalPosition}, {IsEmptySlot}");
            if (!IsVisible)
            {
                throw new InvalidOperationException("Unable to move non visible slot");
            }

            CurrentSlotInformation = slot;
        }
    }
}