using System;
using UnityEngine;

namespace UI.Spells_Panel.Slot_Information
{
    public interface ISlotInformation : IComparable
    {
        public Vector3 LocalScale { get; }
        public Vector3 Position { get; }
    }
}