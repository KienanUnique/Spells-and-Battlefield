using System.Collections.Generic;

public abstract class SpellMechanicEffectImplementationBase : SpellImplementationBase, ISpellMechanicEffect
{
    protected abstract void ApplyEffectToTarget(ISpellInteractable target);
    public virtual void ApplyEffectToTargets(List<ISpellInteractable> targets)
    {
        targets.ForEach(target => ApplyEffectToTarget(target));
    }
}