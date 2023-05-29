using Interfaces;

namespace Common.Abstract_Bases.Character
{
    public interface ICharacterBase : IDamageable, IHealable, IContinuousEffectApplicable, ICharacterInformationProvider
    {
    }
}