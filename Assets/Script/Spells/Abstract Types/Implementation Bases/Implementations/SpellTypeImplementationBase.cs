using System;
using Spells.Implementations_Interfaces.Implementations;
using UnityEngine;

namespace Spells.Abstract_Types.Implementation_Bases.Implementations
{
    public abstract class SpellTypeImplementationBase : ISpellType
    {
        public abstract int TypeID { get; }
        public abstract Color VisualisationColor { get; }

        public override bool Equals(object obj)
        {
            return CompareTo(obj) == 0;
        }

        public override int GetHashCode()
        {
            return TypeID;
        }

        public int CompareTo(object obj)
        {
            if (obj is ISpellType otherSpellType)
            {
                return CompareTo(otherSpellType);
            }

            throw new InvalidCastException();
        }

        public int CompareTo(ISpellType otherSpellType)
        {
            return TypeID.CompareTo(otherSpellType.TypeID);
        }
    }
}