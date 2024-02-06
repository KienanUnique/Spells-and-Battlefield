using Common;
using Common.Readonly_Transform;
using Spells.Controllers.Concrete_Types.Instant;
using Spells.Factory;
using UnityEngine;

namespace Spells.Spell_Handlers.Instant
{
    public abstract class InstantSpellHandlerImplementationBase : SpellsHandlerImplementationBase,
        IInstantSpellHandlerImplementation
    {
        protected readonly IReadonlyLook _look;
        private IInformationAboutInstantSpell _spellToCreate;

        protected InstantSpellHandlerImplementationBase(ICaster caster, ISpellObjectsFactory spellObjectsFactory,
            IReadonlyTransform spellSpawnObject, IReadonlyLook look) : base(caster, spellObjectsFactory,
            spellSpawnObject)
        {
            _look = look;
        }

        protected abstract Quaternion LookRotation { get; }

        public void HandleSpell(IInformationAboutInstantSpell informationAboutInstantSpell)
        {
            HandleStart();
            _spellToCreate = informationAboutInstantSpell;
            PlaySingleActionAnimation(informationAboutInstantSpell.AnimationData);
        }

        public override bool TryInterrupt()
        {
            return false;
        }

        public override void OnSpellCastPartOfAnimationFinished()
        {
            _spellObjectsFactory.Create(_spellToCreate.DataForController, _spellToCreate.PrefabProvider, _caster,
                _spellSpawnObject.Position, LookRotation, _spellSpawnObject);
            HandleEndOfCast();
            HandleEndOfSpell();
        }
    }
}