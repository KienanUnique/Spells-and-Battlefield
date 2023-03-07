using UnityEngine;

[CreateAssetMenu(fileName = "None Movement", menuName = "Spells and Battlefield/Spell System/Movement/None Movement", order = 0)]
public class NoneMovement : SpellMovementScriptableObject
{
#nullable enable
    public override void Move(Rigidbody spellRigidbody, Transform? fromCastObjectTransform, float timePassedFromInitialize)
    {

    }
#nullable disable
}