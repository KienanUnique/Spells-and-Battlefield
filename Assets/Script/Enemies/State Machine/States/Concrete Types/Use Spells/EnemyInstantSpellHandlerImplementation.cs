using Common;
using Common.Readonly_Transform;
using Spells;
using Spells.Factory;
using Spells.Spell_Handlers.Instant;
using UnityEngine;

namespace Enemies.State_Machine.States.Concrete_Types.Use_Spells
{
    public class EnemyInstantSpellHandlerImplementation : InstantSpellHandlerImplementationBase
    {
        public EnemyInstantSpellHandlerImplementation(ICaster caster, ISpellObjectsFactory spellObjectsFactory,
            IReadonlyTransform spellSpawnObject, IReadonlyLook look) : base(caster, spellObjectsFactory,
            spellSpawnObject, look)
        {
        }

        protected override Quaternion LookRotation => Quaternion.LookRotation(_look.LookDirection);
    }
}