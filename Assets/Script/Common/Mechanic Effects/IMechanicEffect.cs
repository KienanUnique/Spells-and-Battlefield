using System.Collections.Generic;
using Interfaces;
using Spells.Implementations_Interfaces;

namespace Common.Mechanic_Effects
{
    public interface IMechanicEffect : ISpellImplementation
    {
        public void ApplyEffectToTargets(IReadOnlyCollection<IInteractable> targets);
        public void ApplyEffectToTarget(IInteractable target);
    }
}