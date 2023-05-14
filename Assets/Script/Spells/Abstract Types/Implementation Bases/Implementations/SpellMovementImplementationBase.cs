using Interfaces;
using Spells.Implementations_Interfaces;
using Spells.Implementations_Interfaces.Implementations;
using UnityEngine;

namespace Spells.Abstract_Types.Implementation_Bases.Implementations
{
    public abstract class SpellMovementImplementationBase : SpellImplementationBase, ISpellMovement
    {
        protected Transform _spellRigidbodyTransform;

        public override void Initialize(Rigidbody spellRigidbody, ICaster caster)
        {
            base.Initialize(spellRigidbody, caster);
            _spellRigidbodyTransform = _spellRigidbody.transform;
        }

        public abstract void UpdatePosition();
    }
}