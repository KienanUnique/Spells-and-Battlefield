using Common;
using UnityEngine;

namespace UI.Spells_Panel.Slot_Information
{
    public class SlotInformation : ISlotInformation
    {
        public SlotInformation(Vector2 sizeDelta, Vector2 anchoredPosition)
        {
            LocalScale = sizeDelta;
            LocalPosition = anchoredPosition;
        }

        public Vector3 LocalScale { get; }
        public Vector3 LocalPosition { get; }

        public int CompareTo(ISlotInformation other)
        {
            var scaleComparison = LocalScale.CompareTo(other.LocalScale);
            return scaleComparison != 0 ? scaleComparison : LocalPosition.CompareTo(other.LocalPosition);
        }
    }
}