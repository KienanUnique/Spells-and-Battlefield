using Spells.Abstract_Types.Implementation_Bases;
using Spells.Abstract_Types.Scriptable_Objects;
using Spells.Implementations_Interfaces;
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
#nullable enable
            public override void UpdatePosition()
            {
                if (_casterTransform == null)
                {
                    return;
                }

                _spellRigidbody.position = _casterTransform.position;
            }
#nullable disable
        }
    }
}