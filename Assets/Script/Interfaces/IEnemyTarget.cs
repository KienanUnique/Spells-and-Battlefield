using Common.Readonly_Rigidbody;
using Common.Readonly_Transform;

namespace Interfaces
{
    public interface IEnemyTarget : IInteractableCharacter, IIdHolder
    {
        public IReadonlyRigidbody MainRigidbody { get; }
        public IReadonlyTransform PointForAiming { get; }
    }
}