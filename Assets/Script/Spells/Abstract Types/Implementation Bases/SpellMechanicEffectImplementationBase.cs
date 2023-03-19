using System.Collections.Generic;
using Interfaces;
using Spells.Implementations_Interfaces;

namespace Spells.Abstract_Types.Implementation_Bases
{
    public abstract class SpellMechanicEffectImplementationBase : SpellImplementationBase, ISpellMechanicEffect
    {
        protected abstract void ApplyEffectToTarget(ISpellInteractable target);

        public virtual void ApplyEffectToTargets(List<ISpellInteractable> targets)
        {
            targets.ForEach(target => ApplyEffectToTarget(target));
        }
    }
}