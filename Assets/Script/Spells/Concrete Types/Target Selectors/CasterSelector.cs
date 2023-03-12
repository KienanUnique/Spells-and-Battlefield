using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Caster Selector", menuName = "Spells and Battlefield/Spell System/Target Selector/Caster Selector", order = 0)]
public class CasterSelector : SpellTargetSelecterScriptableObject
{
    public override ISpellTargetSelecter GetImplementationObject() => new CasterSelectorImplementation();
    private class CasterSelectorImplementation : SpellTargetSelecterImplementationBase
    {
        public override List<ICharacter> SelectTargets(Vector3 spellPosition, ICharacter casterCharacter) => new List<ICharacter>() { casterCharacter };
    }
}