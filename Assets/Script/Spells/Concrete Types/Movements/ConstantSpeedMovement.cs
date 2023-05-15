using Spells.Abstract_Types.Implementation_Bases.Implementations;
using Spells.Abstract_Types.Scriptable_Objects.Parts;
using Spells.Implementations_Interfaces.Implementations;
using UnityEngine;

namespace Spells.Concrete_Types.Movements
{
    [CreateAssetMenu(fileName = "Constant Speed Movement",
        menuName = ScriptableObjectsMenuDirectories.SpellMovementDirectory + "Constant Speed Movement", order = 0)]
    public class ConstantSpeedMovement : SpellMovementScriptableObject
    {
        [SerializeField] private float _speed;
        public override ISpellMovement GetImplementationObject() => new ConstantSpeedMovementImplementation(_speed);

        private class ConstantSpeedMovementImplementation : SpellMovementImplementationBase
        {
            private readonly float _speed;
            public ConstantSpeedMovementImplementation(float speed) => _speed = speed;
            public override void UpdatePosition()
            {
                _spellRigidbody.MovePosition(_spellRigidbodyTransform.position +
                                             _speed * Time.deltaTime * _spellRigidbodyTransform.forward);
            }
        }
    }
}