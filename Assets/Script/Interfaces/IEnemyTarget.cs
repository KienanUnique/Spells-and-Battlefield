using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Common.Readonly_Rigidbody;
using Common.Readonly_Transform;
using Factions;

namespace Interfaces
{
    public interface IEnemyTarget : IInteractableCharacter, IIdHolder, IInitializable
    {
        public IFaction Faction { get; }
        public IReadonlyRigidbody MainRigidbody { get; }
        public IReadonlyTransform PointForAiming { get; }
    }
}