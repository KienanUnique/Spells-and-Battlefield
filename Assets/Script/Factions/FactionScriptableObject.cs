using UnityEngine;

namespace Factions
{
    [CreateAssetMenu(fileName = "Faction", menuName = ScriptableObjectsMenuDirectories.RootDirectory + "Faction",
        order = 0)]
    public class FactionScriptableObject : ScriptableObject, IFaction
    {
        public int Id => GetInstanceID();

        public bool Equals(IFaction other)
        {
            return other != null && Id.Equals(other.Id);
        }

        public OtherFactionRelationship GetRelationshipToOtherFraction(IFaction other)
        {
            OtherFactionRelationship result = Equals(this, other)
                ? OtherFactionRelationship.Friendly
                : OtherFactionRelationship.Aggressive;
            return result;
        }
    }
}