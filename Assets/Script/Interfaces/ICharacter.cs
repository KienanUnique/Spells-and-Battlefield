namespace Interfaces
{
    public interface ICharacter : IInteractable
    {
        public void HandleHeal(int countOfHealthPoints);
        public void HandleDamage(int countOfHealthPoints);
        public CharacterState CurrentCharacterState { get; }
    }
}