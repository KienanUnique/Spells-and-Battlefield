using System;

namespace Factions
{
    public interface IFaction : IEquatable<IFaction>
    {
        public int Id { get; }
        public IFaction RevertFaction { get; }
        public OtherFactionRelationship GetRelationshipToOtherFraction(IFaction other);
    }
}