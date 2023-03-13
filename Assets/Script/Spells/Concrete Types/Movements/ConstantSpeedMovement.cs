using UnityEngine;

[CreateAssetMenu(fileName = "Constant Speed Movement", menuName = "Spells and Battlefield/Spell System/Movement/Constant Speed Movement", order = 0)]
public class ConstantSpeedMovement : SpellMovementScriptableObject
{
    [SerializeField] private float _speed;
    public override ISpellMovement GetImplementationObject() => new ConstantSpeedMovementImplementation(_speed);

    private class ConstantSpeedMovementImplementation : SpellMovementImplementationBase
    {
        private float _speed;
        public ConstantSpeedMovementImplementation(float speed) => _speed = speed;
#nullable enable
        public override void UpdatePosition()
        {
            _spellRigidbody.MovePosition(_spellRigidbodyTransform.position + _speed * Time.deltaTime * _spellRigidbodyTransform.forward);
        }
#nullable disable       
    }
}