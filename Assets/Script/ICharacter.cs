public interface ICharacter
{
    public CharacterTypeEnum CharacterType { get; }
    public void HandleHeal();
    public void HandleDamage();
    public void HandleVelocityBoost();
}