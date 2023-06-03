using System.Collections.Generic;
using Interfaces;
using Pickable_Items;

namespace Spells.Implementations_Interfaces.Implementations
{
    public interface ISpellMechanicEffect : ISpellImplementation
    {
        public void ApplyEffectToTargets(List<ISpellInteractable> targets);
        public void ApplyEffectToTarget(ISpellInteractable target);
    }
}