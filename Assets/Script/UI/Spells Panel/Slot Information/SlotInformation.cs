using System;
using Common;
using UnityEngine;

namespace UI.Spells_Panel.Slot_Information
{
    public class SlotInformation : ISlotInformation
    {
        public SlotInformation(Vector3 scale, Vector3 position)
        {
            LocalScale = scale;
            Position = position;
        }

        public Vector3 LocalScale { get; }
        public Vector3 Position { get; }

        public int CompareTo(object obj)
        {
            if (!(obj is ISlotInformation slotInformation))
            {
                throw new ArgumentException("Object is not a ISlotInformation", nameof(obj));
            }

            var scaleComparison = LocalScale.CompareTo(slotInformation.LocalScale);
            return scaleComparison != 0 ? scaleComparison : Position.CompareTo(slotInformation.Position);
        }
    }
}