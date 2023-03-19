using Interfaces;
using Spells.Implementations_Interfaces;
using UnityEngine;

namespace Spells.Abstract_Types.Implementation_Bases
{
    public abstract class SpellMovementImplementationBase : SpellImplementationBase, ISpellMovement
    {
        protected Transform _spellRigidbodyTransform;

        public override void Initialize(Rigidbody spellRigidbody, Transform fromCastObjectTransform,
            ISpellInteractable casterCharacter)
        {
            base.Initialize(spellRigidbody, fromCastObjectTransform, casterCharacter);
            _spellRigidbodyTransform = _spellRigidbody.transform;
        }
#nullable enable
        public abstract void UpdatePosition();
#nullable disable
    }
}