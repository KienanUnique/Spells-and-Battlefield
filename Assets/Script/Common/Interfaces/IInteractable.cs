using Common.Id_Holder;

namespace Common.Interfaces
{
    public interface IInteractable : IIdHolder
    {
        public bool TryGetComponent<T>(out T component);
    }
}