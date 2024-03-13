using Common.Interfaces;
using Common.Readonly_Transform;
using Spells.Data_For_Spell_Implementation;
using Spells.Implementations_Interfaces;
using Spells.Spell_Interactable_Trigger;
using UnityEngine;

namespace Spells.Abstract_Types.Implementation_Bases
{
    public abstract class SpellImplementationBase : ISpellImplementation
    {
        protected Rigidbody _spellRigidbody;
        protected Transform _spellTransform;
        protected ICaster Caster;
        protected ICoroutineStarter _coroutineStarter;
        protected IReadonlyTransform _castPoint;
        protected ISpellTargetsDetector _targetsDetector;

        public virtual void Initialize(IDataForSpellImplementation data)
        {
            _spellRigidbody = data.SpellRigidbody;
            _spellTransform = data.SpellTransform;
            Caster = data.Caster;
            _coroutineStarter = data.CoroutineStarter;
            _castPoint = data.CastPoint;
            _targetsDetector = data.TargetsDetector;
        }
    }
}