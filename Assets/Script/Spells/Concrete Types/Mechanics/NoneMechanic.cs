using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "None Mechanic", menuName = "Spells and Battlefield/Spell System/Mechanic/None Mechanic", order = 0)]
public class NoneMechanic : SpellMechanicEffectScriptableObject
{
    public override ISpellMechanicEffect GetImplementationObject() => new NoneMechanicImplementation();

    private class NoneMechanicImplementation : ISpellMechanicEffect
    {
        public void ApplyEffectToTargets(List<ICharacter> targets) { }

        public void Initialize(Rigidbody spellRigidbody, Transform fromCastObjectTransform, ICharacter casterCharacter) { }
    }
}