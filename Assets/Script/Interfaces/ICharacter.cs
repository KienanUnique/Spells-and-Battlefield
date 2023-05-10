namespace Interfaces
{
    public interface ICharacter : ICharacterInformation, IInteractable
    {
        public void HandleHeal(int countOfHealthPoints);
        public void HandleDamage(int countOfHealthPoints);
    }
}