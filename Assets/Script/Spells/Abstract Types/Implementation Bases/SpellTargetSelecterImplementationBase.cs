using System.Collections.Generic;
using UnityEngine;

public abstract class SpellTargetSelecterImplementationBase : SpellImplementationBase, ISpellTargetSelecter
{
    public abstract List<ISpellInteractable> SelectTargets();
}