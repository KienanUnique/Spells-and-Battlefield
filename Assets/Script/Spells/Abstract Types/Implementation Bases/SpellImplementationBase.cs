using Interfaces;
using Spells.Implementations_Interfaces;
using UnityEngine;

namespace Spells.Abstract_Types.Implementation_Bases
{
    public abstract class SpellImplementationBase : ISpellImplementation
    {
        protected Rigidbody _spellRigidbody;
        protected Transform _casterTransform;
        protected ISpellInteractable _casterInterface;

        public virtual void Initialize(Rigidbody spellRigidbody, Transform casterTransform,
            ISpellInteractable casterInterface)
        {
            _spellRigidbody = spellRigidbody;
            _casterTransform = casterTransform;
            _casterInterface = casterInterface;
        }
    }
}