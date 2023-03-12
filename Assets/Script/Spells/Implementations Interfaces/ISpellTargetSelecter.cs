using System.Collections.Generic;
using UnityEngine;

public interface ISpellTargetSelecter
{
    public List<ICharacter> SelectTargets(Vector3 spellPosition, ICharacter casterCharacter);
}