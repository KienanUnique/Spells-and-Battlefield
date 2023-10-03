using Common.Id_Holder;
using Common.Readonly_Transform;

namespace Common.Interfaces
{
    public interface IInteractable : IIdHolder
    {
        public IReadonlyTransform MainTransform { get; }
        public bool TryGetComponent<T>(out T component);
    }
}