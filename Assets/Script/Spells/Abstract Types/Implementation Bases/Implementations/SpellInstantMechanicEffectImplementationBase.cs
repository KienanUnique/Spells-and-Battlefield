using System.Collections.Generic;
using Interfaces;
using Spells.Implementations_Interfaces;
using Spells.Implementations_Interfaces.Implementations;

namespace Spells.Abstract_Types.Implementation_Bases.Implementations
{
    public abstract class SpellInstantMechanicEffectImplementationBase : SpellImplementationBase, ISpellMechanicEffect
    {
        public abstract void ApplyEffectToTarget(ISpellInteractable target);

        public virtual void ApplyEffectToTargets(List<ISpellInteractable> targets)
        {
            targets.ForEach(ApplyEffectToTarget);
        }
    }
}