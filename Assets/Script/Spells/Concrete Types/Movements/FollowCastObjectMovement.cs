using Enemies.Look_Point_Calculator;
using Enemies.Look_Point_Calculator.Concrete_Types;
using Spells.Abstract_Types.Implementation_Bases.Implementations;
using Spells.Abstract_Types.Scriptable_Objects.Parts;
using Spells.Implementations_Interfaces.Implementations;
using UnityEngine;

namespace Spells.Concrete_Types.Movements
{
    [CreateAssetMenu(fileName = "Follow Cast Object Movement",
        menuName = ScriptableObjectsMenuDirectories.SpellMovementDirectory + "Follow Cast Object Movement", order = 0)]
    public class FollowCastObjectMovement : SpellMovementScriptableObject
    {
        public override ISpellMovementWithLookPointCalculator GetImplementationObject()
        {
            return new FollowCastObjectMovementImplementation();
        }

        private class FollowCastObjectMovementImplementation : SpellMovementImplementationBase
        {
            public override void UpdatePosition()
            {
                if (Caster == null)
                {
                    return;
                }

                _spellRigidbody.position = Caster.CurrentPosition;
            }

            public override ILookPointCalculator GetLookPointCalculator()
            {
                return new FollowVelocityDirectionLookPointCalculator();
            }
        }
    }
}