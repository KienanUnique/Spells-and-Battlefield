using Interfaces;
using Spells.Implementations_Interfaces;
using UnityEngine;

namespace Spells.Abstract_Types.Implementation_Bases
{
    public abstract class SpellImplementationBase : ISpellImplementation
    {
        protected Rigidbody _spellRigidbody;
        protected ICaster Caster;

        public virtual void Initialize(Rigidbody spellRigidbody, ICaster caster)
        {
            _spellRigidbody = spellRigidbody;
            Caster = caster;
        }
    }
}