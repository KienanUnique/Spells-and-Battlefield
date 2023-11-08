using System.Collections;
using Enemies.Look_Point_Calculator;
using Enemies.Look_Point_Calculator.Concrete_Types;
using Spells.Abstract_Types.Implementation_Bases.Implementations;
using Spells.Abstract_Types.Scriptable_Objects.Parts;
using Spells.Implementations_Interfaces.Implementations;
using UnityEngine;

namespace Spells.Concrete_Types.Movements
{
    [CreateAssetMenu(fileName = "Rotating Around Caster Movement",
        menuName = ScriptableObjectsMenuDirectories.SpellMovementDirectory + "Rotating Around Caster Movement",
        order = 0)]
    public class RotatingAroundCasterMovement : SpellMovementScriptableObject
    {
        [SerializeField] private float _speed = 700;
        [SerializeField] private float _radius = 3;

        public override ISpellMovementWithLookPointCalculator GetImplementationObject()
        {
            return new RotatingAroundCasterMovementImplementation(_speed, _radius);
        }

        private class RotatingAroundCasterMovementImplementation : SpellMovementImplementationBase
        {
            private readonly float _radius;
            private readonly float _speed;
            private float _currentAngle;
            private Coroutine _moveCoroutine;

            public RotatingAroundCasterMovementImplementation(float speed, float radius)
            {
                _speed = speed;
                _radius = radius;
            }

            public override ILookPointCalculator GetLookPointCalculator()
            {
                return new KeepLookDirectionLookPointCalculator();
            }

            public override void StartMoving()
            {
                if (Caster == null)
                {
                    return;
                }

                _moveCoroutine = _coroutineStarter.StartCoroutine(RotateAroundCastObject());
            }

            public override void StopMoving()
            {
                _coroutineStarter.StopCoroutine(_moveCoroutine);
            }

            private IEnumerator RotateAroundCastObject()
            {
                while (true)
                {
                    _currentAngle += _speed * Time.deltaTime;
                    if (_currentAngle > 360)
                    {
                        _currentAngle -= 360;
                    }

                    Vector3 orbit = Vector3.forward * _radius;
                    orbit = Quaternion.Euler(0, _currentAngle, 0) * orbit;
                    _spellRigidbody.MovePosition(Caster.MainTransform.Position + orbit);
                    yield return null;
                }
            }
        }
    }
}