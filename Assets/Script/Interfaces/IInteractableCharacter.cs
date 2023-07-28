namespace Interfaces
{
    public interface IInteractableCharacter : ICharacterInformationProvider, IDamageable, IHealable,
        IContinuousEffectApplicable, IInteractable, IIdHolder
    {
    }
}