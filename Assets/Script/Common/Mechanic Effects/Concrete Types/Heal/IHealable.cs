using Common.Mechanic_Effects.Source;

namespace Common.Mechanic_Effects.Concrete_Types.Heal
{
    public interface IHealable
    {
        public void HandleHeal(int countOfHitPoints, IEffectSourceInformation effectSourceInformation);
    }
}