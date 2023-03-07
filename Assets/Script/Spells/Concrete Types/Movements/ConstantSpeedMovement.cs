using UnityEngine;

[CreateAssetMenu(fileName = "Constant Speed Movement", menuName = "Spells and Battlefield/Spell System/Movement/Constant Speed Movement", order = 0)]
public class ConstantSpeedMovement : SpellMovementScriptableObject
{
    [SerializeField] private float _speed;
#nullable enable
    public override void Move(Rigidbody spellRigidbody, Transform? fromCastObjectTransform, float timePassedFromInitialize)
    {
        spellRigidbody.MovePosition(spellRigidbody.transform.position + _speed * Time.deltaTime * spellRigidbody.transform.forward);
    }
#nullable disable
}