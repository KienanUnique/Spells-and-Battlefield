using Common.Readonly_Transform;
using Spells.Controllers.Concrete_Types.Continuous;
using Spells.Controllers.Concrete_Types.Continuous.Data_For_Controller;
using Spells.Controllers.Concrete_Types.Continuous.Prefab_Provider;
using Spells.Controllers.Concrete_Types.Instant;
using Spells.Controllers.Concrete_Types.Instant.Data_For_Controller;
using Spells.Controllers.Concrete_Types.Instant.Prefab_Provider;
using UnityEngine;

namespace Spells.Factory
{
    public interface ISpellObjectsFactory
    {
        public IContinuousSpellController Create(IDataForContinuousSpellController spellControllerData,
            IContinuousSpellPrefabProvider prefabProvider, ICaster caster, IReadonlyTransform castPoint);

        public IInstantSpellController Create(IDataForInstantSpellController spellControllerData,
            IInstantSpellPrefabProvider prefabProvider, ICaster caster, Vector3 spawnPosition, Quaternion spawnRotation,
            IReadonlyTransform castPoint);
    }
}