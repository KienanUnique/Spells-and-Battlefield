using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "None Target Selector", menuName = "Spells and Battlefield/Spell System/Target Selector/None Target Selector", order = 0)]
public class NoneTargetSelector : SpellTargetSelecterScriptableObject
{
    public override ISpellTargetSelecter GetImplementationObject() => new NoneSelectorImplementation();
    private class NoneSelectorImplementation : ISpellTargetSelecter
    {
        public void Initialize(Rigidbody spellRigidbody, Transform fromCastObjectTransform, ICharacter casterCharacter) { }

        public List<ICharacter> SelectTargets() => new List<ICharacter>();
    }
}