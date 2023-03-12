using UnityEngine;

[CreateAssetMenu(fileName = "None Movement", menuName = "Spells and Battlefield/Spell System/Movement/None Movement", order = 0)]
public class NoneMovement : SpellMovementScriptableObject
{
    public override ISpellMovement GetImplementationObject() => new NoneMovementImplementation();

    private class NoneMovementImplementation : ISpellMovement
    {
        public void Move(Rigidbody spellRigidbody, Transform fromCastObjectTransform, float timePassedFromInitialize)
        {
            throw new System.NotImplementedException();
        }
    }
}