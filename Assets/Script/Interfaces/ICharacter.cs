namespace Interfaces
{
    public interface ICharacter : IInteractable
    {
        public CharacterState CurrentCharacterState { get; }
        public void HandleHeal(int countOfHealthPoints);
        public void HandleDamage(int countOfHealthPoints);
    }
}