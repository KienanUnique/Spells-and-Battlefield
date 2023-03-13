using UnityEngine;

[CreateAssetMenu(fileName = "Follow Cast Object Movement", menuName = "Spells and Battlefield/Spell System/Movement/Follow Cast Object Movement", order = 0)]
public class FollowCastObjectMovement : SpellMovementScriptableObject
{
    public override ISpellMovement GetImplementationObject() => new FollowCastObjectMovementImplementation();

    private class FollowCastObjectMovementImplementation : SpellMovementImplementationBase
    {
#nullable enable
        public override void UpdatePosition()
        {
            if (_fromCastObjectTransform == null)
            {
                return;
            }
            _spellRigidbody.position = _fromCastObjectTransform.position;
        }
#nullable disable       
    }
}