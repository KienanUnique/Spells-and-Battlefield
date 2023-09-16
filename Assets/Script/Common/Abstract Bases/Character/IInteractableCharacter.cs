using Common.Id_Holder;
using Common.Interfaces;
using Common.Mechanic_Effects.Concrete_Types.Damage;
using Common.Mechanic_Effects.Concrete_Types.Heal;
using Common.Mechanic_Effects.Continuous_Effect;

namespace Common.Abstract_Bases.Character
{
    public interface IInteractableCharacter : ICharacterInformationProvider,
        IDamageable,
        IHealable,
        IContinuousEffectApplicable,
        IInteractable,
        IIdHolder
    {
    }
}