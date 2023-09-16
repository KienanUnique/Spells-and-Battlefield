using Common.Abstract_Bases.Character;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Common.Id_Holder;
using Common.Readonly_Rigidbody;
using Common.Readonly_Transform;
using Factions;

namespace Enemies
{
    public interface IEnemyTarget : IInteractableCharacter, IIdHolder, IInitializable
    {
        public IFaction Faction { get; }
        public IReadonlyRigidbody MainRigidbody { get; }
        public IReadonlyTransform PointForAiming { get; }
    }
}