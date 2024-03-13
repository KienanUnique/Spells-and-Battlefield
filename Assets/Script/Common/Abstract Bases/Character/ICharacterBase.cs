using Common.Mechanic_Effects.Concrete_Types.Change_Fraction;
using Common.Mechanic_Effects.Concrete_Types.Damage;
using Common.Mechanic_Effects.Concrete_Types.Heal;
using Common.Mechanic_Effects.Continuous_Effect;

namespace Common.Abstract_Bases.Character
{
    public interface ICharacterBase : IDamageable, IHealable, IContinuousEffectApplicable, ICharacter, IFactionChangeable
    {
    }
}