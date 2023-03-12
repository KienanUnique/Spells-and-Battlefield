using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "None Target Selector", menuName = "Spells and Battlefield/Spell System/Target Selector/None Target Selector", order = 0)]
public class NoneTargetSelector : SpellTargetSelecterScriptableObject
{
    public override ISpellTargetSelecter GetImplementationObject() => new NoneSelectorImplementation();
    private class NoneSelectorImplementation : ISpellTargetSelecter
    {
        public List<ICharacter> SelectTargets(Vector3 spellPosition, ICharacter casterCharacter) => new List<ICharacter>();
    }
}