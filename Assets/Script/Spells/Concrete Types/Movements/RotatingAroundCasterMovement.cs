using Spells.Abstract_Types.Implementation_Bases;
using Spells.Abstract_Types.Scriptable_Objects;
using Spells.Implementations_Interfaces;
using UnityEngine;

namespace Spells.Concrete_Types.Movements
{
    [CreateAssetMenu(fileName = "Rotating Around Caster Movement",
        menuName = "Spells and Battlefield/Spell System/Movement/Rotating Around Caster Movement", order = 0)]
    public class RotatingAroundCasterMovement : SpellMovementScriptableObject
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _radius;

        public override ISpellMovement GetImplementationObject() =>
            new RotatingAroundCasterMovementImplementation(_speed, _radius);

        private class RotatingAroundCasterMovementImplementation : SpellMovementImplementationBase
        {
            private readonly float _speed;
            private readonly float _radius;

            public RotatingAroundCasterMovementImplementation(float speed, float radius)
            {
                _speed = speed;
                _radius = radius;
            }
#nullable enable
            public override void UpdatePosition()
            {
                if (_casterTransform == null)
                {
                    return;
                }

                var fromCastObjectPosition = _casterTransform.position;
                if (!Mathf.Approximately(_radius,
                        Vector3.Distance(fromCastObjectPosition, _spellRigidbodyTransform.position)))
                {
                    var direction = (_spellRigidbodyTransform.position - fromCastObjectPosition).normalized;
                    var needPosition = fromCastObjectPosition + direction * _radius;
                    needPosition.y = fromCastObjectPosition.y;
                    _spellRigidbodyTransform.position = needPosition;
                }

                _spellRigidbodyTransform.RotateAround(fromCastObjectPosition, _spellRigidbodyTransform.up,
                    _speed * Time.fixedDeltaTime);
            }
#nullable disable
        }
    }
}