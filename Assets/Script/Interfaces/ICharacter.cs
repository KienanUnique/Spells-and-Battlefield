namespace Interfaces
{
    public interface ICharacter : ICharacterInformation, IIdHolder
    {
        public void HandleHeal(int countOfHealthPoints);
        public void HandleDamage(int countOfHealthPoints);
    }
}