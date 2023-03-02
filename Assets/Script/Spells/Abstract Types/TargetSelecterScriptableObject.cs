using System.Collections.Generic;
using UnityEngine;

public abstract class TargetSelecterScriptableObject : ScriptableObject
{
    public abstract List<ICharacter> SelectTargets(Vector3 spellPosition);
}