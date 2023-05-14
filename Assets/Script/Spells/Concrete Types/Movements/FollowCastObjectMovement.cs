using Spells.Abstract_Types.Implementation_Bases.Implementations;
using Spells.Abstract_Types.Scriptable_Objects.Parts;
using Spells.Implementations_Interfaces;
using Spells.Implementations_Interfaces.Implementations;
using UnityEngine;

namespace Spells.Concrete_Types.Movements
{
    [CreateAssetMenu(fileName = "Follow Cast Object Movement",
        menuName = "Spells and Battlefield/Spell System/Movement/Follow Cast Object Movement", order = 0)]
    public class FollowCastObjectMovement : SpellMovementScriptableObject
    {
        public override ISpellMovement GetImplementationObject() => new FollowCastObjectMovementImplementation();

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
        }
    }
}