using Common.Interfaces;

namespace Common.Mechanic_Effects.Continuous_Effect.Factory
{
    public interface IContinuousEffectFactory
    {
        public IContinuousEffect Create(IInteractable target);
    }
}