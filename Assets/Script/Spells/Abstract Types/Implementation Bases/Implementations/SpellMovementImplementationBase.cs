using Enemies.Look_Point_Calculator;
using Interfaces;
using Spells.Implementations_Interfaces.Implementations;
using UnityEngine;

namespace Spells.Abstract_Types.Implementation_Bases.Implementations
{
    public abstract class SpellMovementImplementationBase : SpellImplementationBase,
        ISpellMovementWithLookPointCalculator
    {
        protected Transform _spellRigidbodyTransform;

        public abstract void UpdatePosition();
        public abstract ILookPointCalculator GetLookPointCalculator();

        public override void Initialize(Rigidbody spellRigidbody, ICaster caster)
        {
            base.Initialize(spellRigidbody, caster);
            _spellRigidbodyTransform = _spellRigidbody.transform;
        }
    }
}