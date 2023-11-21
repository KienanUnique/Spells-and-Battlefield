using Common.Readonly_Transform;
using Player.Look;
using Spells.Controllers.Concrete_Types.Instant;
using Spells.Factory;
using UnityEngine;

namespace Spells.Spell_Handlers.Instant
{
    public class InstantSpellHandlerImplementation : SpellsHandlerImplementationBase, IInstantSpellHandlerImplementation
    {
        private IInformationAboutInstantSpell _spellToCreate;

        public InstantSpellHandlerImplementation(ICaster caster, ISpellObjectsFactory spellObjectsFactory,
            IReadonlyTransform spellSpawnObject, IReadonlyPlayerLook look) : base(caster, spellObjectsFactory,
            spellSpawnObject, look)
        {
        }

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
                _spellSpawnObject.Position,
                Quaternion.LookRotation(_look.CameraLookPointPosition - _spellSpawnObject.Position), _spellSpawnObject);
            HandleEndOfCast();
            HandleEndOfSpell();
        }
    }
}