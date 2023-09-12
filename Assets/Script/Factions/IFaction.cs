using System;

namespace Factions
{
    public interface IFaction : IEquatable<IFaction>
    {
        public int Id { get; }
        public OtherFactionRelationship GetRelationshipToOtherFraction(IFaction other);
    }
}