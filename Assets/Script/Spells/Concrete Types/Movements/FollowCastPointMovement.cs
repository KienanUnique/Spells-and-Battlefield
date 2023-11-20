using System.Collections;
using Enemies.Look_Point_Calculator;
using Enemies.Look_Point_Calculator.Concrete_Types;
using Spells.Abstract_Types.Implementation_Bases.Implementations;
using Spells.Abstract_Types.Scriptable_Objects.Parts;
using Spells.Implementations_Interfaces.Implementations;
using UnityEngine;

namespace Spells.Concrete_Types.Movements
{
    [CreateAssetMenu(fileName = "Follow Cast Point Movement",
        menuName = ScriptableObjectsMenuDirectories.SpellMovementDirectory + "Follow Cast Point Movement", order = 0)]
    public class FollowCastPointMovement : SpellMovementScriptableObject
    {
        public override ISpellMovementWithLookPointCalculator GetImplementationObject()
        {
            return new FollowCastPointMovementImplementation();
        }

        private class FollowCastPointMovementImplementation : SpellMovementImplementationBase
        {
            private Coroutine _moveCoroutine;

            public override ILookPointCalculator GetLookPointCalculator()
            {
                return new FollowTargetLookPointCalculator();
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
                    _spellTransform.position = _castPoint.Position;
                    _spellTransform.forward = _castPoint.Forward;
                    yield return null;
                }
            }
        }
    }
}