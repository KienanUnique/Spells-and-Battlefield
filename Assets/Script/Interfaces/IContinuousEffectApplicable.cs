using Common.Mechanic_Effects.Continuous_Effect;

namespace Interfaces
{
    public interface IContinuousEffectApplicable
    {
        public void ApplyContinuousEffect(IAppliedContinuousEffect effect);
    }
}