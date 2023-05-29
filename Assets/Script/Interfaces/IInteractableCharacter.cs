namespace Interfaces
{
    public interface IInteractableCharacter : ICharacterInformationProvider, IDamageable, IHealable,
        IContinuousEffectApplicable, IIdHolder
    {
    }
}