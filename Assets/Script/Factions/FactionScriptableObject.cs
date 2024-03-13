using UnityEngine;

namespace Factions
{
    [CreateAssetMenu(fileName = "Faction", menuName = ScriptableObjectsMenuDirectories.RootDirectory + "Faction",
        order = 0)]
    public class FactionScriptableObject : ScriptableObject, IFaction
    {
        [SerializeField] private FactionScriptableObject _revertFaction;
        public int Id => GetInstanceID();
        public IFaction RevertFaction => _revertFaction;

        public bool Equals(IFaction other)
        {
            return other != null && Id.Equals(other.Id);
        }

        public OtherFactionRelationship GetRelationshipToOtherFraction(IFaction other)
        {
            var result = Equals(this, other) ? OtherFactionRelationship.Friendly : OtherFactionRelationship.Aggressive;
            return result;
        }
    }
}