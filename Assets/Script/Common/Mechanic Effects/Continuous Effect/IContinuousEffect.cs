using Interfaces;

namespace Common.Mechanic_Effects.Continuous_Effect
{
    public interface IContinuousEffect : IAppliedContinuousEffect
    {
        public void SetTarget(IInteractable target);
    }
}