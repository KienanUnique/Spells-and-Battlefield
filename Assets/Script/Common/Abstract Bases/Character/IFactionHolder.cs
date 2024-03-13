using Factions;

namespace Common.Abstract_Bases.Character
{
    public interface IFactionHolder
    {
        IFaction Faction { get; }
    }
}