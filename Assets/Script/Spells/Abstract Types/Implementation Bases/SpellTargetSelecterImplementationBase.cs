using System.Collections.Generic;
using UnityEngine;

public abstract class SpellTargetSelecterImplementationBase : ISpellTargetSelecter
{
    public abstract List<ICharacter> SelectTargets(Vector3 spellPosition, ICharacter casterCharacter);
}