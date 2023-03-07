using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "None Target Selector", menuName = "Spells and Battlefield/Spell System/Target Selector/None Target Selector", order = 0)]
public class NoneTargetSelector : TargetSelecterScriptableObject
{
    public override List<ICharacter> SelectTargets(Vector3 spellPosition) => new List<ICharacter>();
}