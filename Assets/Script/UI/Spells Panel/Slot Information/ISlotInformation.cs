using System;
using UnityEngine;

namespace UI.Spells_Panel.Slot_Information
{
    public interface ISlotInformation : IComparable<ISlotInformation>
    {
        public Vector2 LocalScale { get; }
        public Vector2 LocalPosition { get; }
    }
}