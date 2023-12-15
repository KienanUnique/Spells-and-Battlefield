using Enemies.Look_Point_Calculator;
using Spells.Data_For_Spell_Implementation;
using Spells.Implementations_Interfaces.Implementations;
using UnityEngine;

namespace Spells.Abstract_Types.Implementation_Bases.Implementations
{
    public abstract class SpellMovementImplementationBase : SpellImplementationBase,
        ISpellMovementWithLookPointCalculator
    {
        protected Transform _spellTransform;

        public override void Initialize(IDataForSpellImplementation data)
        {
            base.Initialize(data);
            _spellTransform = _spellRigidbody.transform;
        }

        public abstract ILookPointCalculator GetLookPointCalculator();
        public abstract void StartMoving();

        public abstract void StopMoving();
    }
}