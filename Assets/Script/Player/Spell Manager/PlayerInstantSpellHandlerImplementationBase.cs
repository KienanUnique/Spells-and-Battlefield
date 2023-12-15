using Common;
using Common.Readonly_Transform;
using Spells;
using Spells.Factory;
using Spells.Spell_Handlers.Instant;
using UnityEngine;

namespace Player.Spell_Manager
{
    public class PlayerInstantSpellHandlerImplementationBase : InstantSpellHandlerImplementationBase
    {
        public PlayerInstantSpellHandlerImplementationBase(ICaster caster, ISpellObjectsFactory spellObjectsFactory,
            IReadonlyTransform spellSpawnObject, IReadonlyLook look) : base(caster, spellObjectsFactory,
            spellSpawnObject, look)
        {
        }

        protected override Quaternion LookRotation =>
            Quaternion.LookRotation(_look.LookPointPosition - _spellSpawnObject.Position);
    }
}