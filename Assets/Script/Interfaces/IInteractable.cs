namespace Interfaces
{
    public interface IInteractable : IIdHolder
    {
        public bool TryGetComponent<T>(out T component);
    }
}