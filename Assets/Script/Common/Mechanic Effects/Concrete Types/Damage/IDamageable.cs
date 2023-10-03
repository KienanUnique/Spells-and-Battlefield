using Common.Mechanic_Effects.Source;

namespace Common.Mechanic_Effects.Concrete_Types.Damage
{
    public interface IDamageable
    {
        public void HandleDamage(int countOfHealthPoints, IEffectSourceInformation effectSourceInformation);
    }
}