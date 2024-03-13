using System;
using Factions;

namespace Common.Abstract_Bases.Character
{
    public interface ICharacterWithFaction : IFactionHolder
    {
        event Action<IFaction> FactionChanged;
    }
}