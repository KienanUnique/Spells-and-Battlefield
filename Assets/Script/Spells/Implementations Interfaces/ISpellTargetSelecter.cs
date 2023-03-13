using System.Collections.Generic;
using UnityEngine;

public interface ISpellTargetSelecter : ISpellImplementation
{
    public List<ICharacter> SelectTargets();
}