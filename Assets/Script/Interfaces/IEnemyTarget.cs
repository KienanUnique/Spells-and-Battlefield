using Common.Readonly_Rigidbody;

namespace Interfaces
{
    public interface IEnemyTarget : IInteractableCharacter, IIdHolder
    {
        public IReadonlyRigidbody MainRigidbody { get; }
    }
}