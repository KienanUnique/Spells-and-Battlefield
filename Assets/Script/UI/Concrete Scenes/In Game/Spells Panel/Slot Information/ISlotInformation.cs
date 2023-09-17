using System;
using UnityEngine;

namespace UI.Concrete_Scenes.In_Game.Spells_Panel.Slot_Information
{
    public interface ISlotInformation : IComparable<ISlotInformation>
    {
        public Vector3 LocalScale { get; }
        public Vector3 LocalPosition { get; }
    }
}