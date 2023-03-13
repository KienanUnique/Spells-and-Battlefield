using System.Collections.Generic;

public interface ISpellMechanicEffect : ISpellImplementation
{
    public void ApplyEffectToTargets(List<ISpellInteractable> targets);
}