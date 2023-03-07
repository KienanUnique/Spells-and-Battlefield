public interface ICharacter
{
    public CharacterTypeEnum CharacterType { get; }
    public void HandleHeal(int countOfHealPoints);
    public void HandleDamage(int countOfHealPoints);
    public void HandleVelocityBoost();
}