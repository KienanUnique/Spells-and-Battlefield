using Common.Abstract_Bases.Character;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Common.Abstract_Bases.Movement;
using Common.Id_Holder;
using Common.Readonly_Transform;
using Factions;

namespace Enemies
{
    public interface IEnemyTarget : IInteractableCharacter, IIdHolder, IInitializable, IPhysicsInformation
    {
        public IFaction Faction { get; }
        public IReadonlyTransform PointForAiming { get; }
    }
}