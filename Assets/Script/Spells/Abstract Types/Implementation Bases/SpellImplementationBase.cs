using Common.Interfaces;
using Spells.Implementations_Interfaces;
using UnityEngine;

namespace Spells.Abstract_Types.Implementation_Bases
{
    public abstract class SpellImplementationBase : ISpellImplementation
    {
        protected Rigidbody _spellRigidbody;
        protected ICaster Caster;
        protected ICoroutineStarter _coroutineStarter;

        public virtual void Initialize(Rigidbody spellRigidbody, ICaster caster, ICoroutineStarter coroutineStarter)
        {
            _spellRigidbody = spellRigidbody;
            Caster = caster;
            _coroutineStarter = coroutineStarter;
        }
    }
}