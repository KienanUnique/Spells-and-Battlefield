namespace Interfaces
{
    public interface ICharacter : IInteractable
    {
        public ValueWithReactionOnChange<CharacterState> CurrentCharacterState { get; }
        public void HandleHeal(int countOfHealthPoints);
        public void HandleDamage(int countOfHealthPoints);
    }
}