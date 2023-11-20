using System.Collections;
using Enemies.Look_Point_Calculator;
using Enemies.Look_Point_Calculator.Concrete_Types;
using Spells.Abstract_Types.Implementation_Bases.Implementations;
using Spells.Abstract_Types.Scriptable_Objects.Parts;
using Spells.Implementations_Interfaces.Implementations;
using UnityEngine;

namespace Spells.Concrete_Types.Movements
{
    [CreateAssetMenu(fileName = "Follow Caster Movement",
        menuName = ScriptableObjectsMenuDirectories.SpellMovementDirectory + "Follow Caster Movement", order = 0)]
    public class FollowCasterMovement : SpellMovementScriptableObject
    {
        public override ISpellMovementWithLookPointCalculator GetImplementationObject()
        {
            return new FollowCasterMovementImplementation();
        }

        private class FollowCasterMovementImplementation : SpellMovementImplementationBase
        {
            private Coroutine _moveCoroutine;

            public override ILookPointCalculator GetLookPointCalculator()
            {
                return new FollowVelocityDirectionLookPointCalculator();
            }

            public override void StartMoving()
            {
                _moveCoroutine = _coroutineStarter.StartCoroutine(FollowCastObject());
            }

            public override void StopMoving()
            {
                _coroutineStarter.StopCoroutine(_moveCoroutine);
            }

            private IEnumerator FollowCastObject()
            {
                while (true)
                {
                    _spellRigidbody.position = Caster.MainTransform.Position;
                    yield return null;
                }
            }
        }
    }
}