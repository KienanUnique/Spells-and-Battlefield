using System;
using UnityEngine;

namespace UI.Spells_Panel.Slot_Information
{
    public interface ISlotInformation : IComparable<ISlotInformation>
    {
        public Vector2 SizeDelta { get; }
        public Vector2 AnchoredPosition { get; }
    }
}