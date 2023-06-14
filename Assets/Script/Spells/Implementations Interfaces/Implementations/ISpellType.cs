using System;
using UnityEngine;

namespace Spells.Implementations_Interfaces.Implementations
{
    public interface ISpellType : IComparable, IComparable<ISpellType>
    {
        public int TypeID { get; }
        public Color VisualisationColor { get; }
    }
}