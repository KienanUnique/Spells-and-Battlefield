using System.Collections.Generic;
using Common.Interfaces;
using Common.Mechanic_Effects.Source;

namespace Common.Mechanic_Effects
{
    public interface IMechanicEffect
    {
        public void ApplyEffectToTargets(IEnumerable<IInteractable> targets,
            IEffectSourceInformation sourceInformation);

        public void ApplyEffectToTarget(IInteractable target, IEffectSourceInformation sourceInformation);
    }
}