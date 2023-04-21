using Spells.Abstract_Types.Implementation_Bases;
using Spells.Abstract_Types.Scriptable_Objects;
using Spells.Implementations_Interfaces;
using UnityEngine;

namespace Spells.Concrete_Types.Movements
{
    [CreateAssetMenu(fileName = "Constant Speed Movement",
        menuName = "Spells and Battlefield/Spell System/Movement/Constant Speed Movement", order = 0)]
    public class ConstantSpeedMovement : SpellMovementScriptableObject
    {
        [SerializeField] private float _speed;
        public override ISpellMovement GetImplementationObject() => new ConstantSpeedMovementImplementation(_speed);

        private class ConstantSpeedMovementImplementation : SpellMovementImplementationBase
        {
            private readonly float _speed;
            public ConstantSpeedMovementImplementation(float speed) => _speed = speed;
#nullable enable
            public override void UpdatePosition()
            {
                _spellRigidbody.MovePosition(_spellRigidbodyTransform.position +
                                             _speed * Time.deltaTime * _spellRigidbodyTransform.forward);
            }
#nullable disable
        }
    }
}