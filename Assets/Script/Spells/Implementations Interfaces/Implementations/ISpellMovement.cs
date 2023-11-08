namespace Spells.Implementations_Interfaces.Implementations
{
    public interface ISpellMovement : ISpellImplementation
    {
        public void StartMoving();
        public void StopMoving();
    }
}