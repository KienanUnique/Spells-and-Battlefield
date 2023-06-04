using Common;
using Common.Readonly_Transform;

namespace Interfaces
{
    public interface IEnemyTarget : IInteractableCharacter, IIdHolder
    {
        public IReadonlyTransform MainTransform { get; }
    }
}