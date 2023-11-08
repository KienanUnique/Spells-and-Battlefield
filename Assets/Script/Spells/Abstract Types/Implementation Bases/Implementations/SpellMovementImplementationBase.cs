using Common.Interfaces;
using Enemies.Look_Point_Calculator;
using Spells.Implementations_Interfaces.Implementations;
using UnityEngine;

namespace Spells.Abstract_Types.Implementation_Bases.Implementations
{
    public abstract class SpellMovementImplementationBase : SpellImplementationBase,
        ISpellMovementWithLookPointCalculator
    {
        protected Transform _spellTransform;
        protected GameObject _spellGameObject;

        public abstract ILookPointCalculator GetLookPointCalculator();

        public override void Initialize(Rigidbody spellRigidbody, ICaster caster, ICoroutineStarter coroutineStarter)
        {
            base.Initialize(spellRigidbody, caster, coroutineStarter);
            _spellTransform = _spellRigidbody.transform;
            _spellGameObject = _spellRigidbody.gameObject;
        }

        public abstract void StartMoving();
        public abstract void StopMoving();
    }
}