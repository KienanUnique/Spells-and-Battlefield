using Interfaces;

namespace Spells.Continuous_Effect
{
    public interface IContinuousEffect : IAppliedContinuousEffect
    {
        public void SetTarget(ISpellInteractable target);
    }
}