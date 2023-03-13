using System.Collections.Generic;

public interface ISpellTargetSelecter : ISpellImplementation
{
    public List<ISpellInteractable> SelectTargets();
}