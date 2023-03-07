using UnityEngine;

[CreateAssetMenu(fileName = "Follow Cast Object Movement", menuName = "Spells and Battlefield/Spell System/Movement/Follow Cast Object Movement", order = 0)]
public class FollowCastObjectMovement : SpellMovementScriptableObject
{
#nullable enable
    public override void Move(Rigidbody spellRigidbody, Transform? fromCastObjectTransform, float timePassedFromInitialize)
    {
        if (fromCastObjectTransform == null)
        {
            return;
        }
        spellRigidbody.position = fromCastObjectTransform.position;
    }
#nullable disable
}