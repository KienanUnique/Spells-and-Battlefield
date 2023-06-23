using Common;
using UnityEngine;

namespace UI.Spells_Panel.Slot_Information
{
    public class SlotInformation : ISlotInformation
    {
        public SlotInformation(Vector2 sizeDelta, Vector2 anchoredPosition)
        {
            SizeDelta = sizeDelta;
            AnchoredPosition = anchoredPosition;
        }

        public Vector2 SizeDelta { get; }
        public Vector2 AnchoredPosition { get; }

        public int CompareTo(ISlotInformation other)
        {
            var scaleComparison = SizeDelta.CompareTo(other.SizeDelta);
            return scaleComparison != 0 ? scaleComparison : AnchoredPosition.CompareTo(other.AnchoredPosition);
        }
    }
}