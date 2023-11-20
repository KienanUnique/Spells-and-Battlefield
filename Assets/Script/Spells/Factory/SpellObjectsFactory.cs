using Common.Abstract_Bases.Factories;
using Common.Readonly_Transform;
using Spells.Controllers;
using Spells.Controllers.Concrete_Types.Continuous;
using Spells.Controllers.Concrete_Types.Continuous.Data_For_Controller;
using Spells.Controllers.Concrete_Types.Continuous.Prefab_Provider;
using Spells.Controllers.Concrete_Types.Instant;
using Spells.Controllers.Concrete_Types.Instant.Data_For_Controller;
using Spells.Controllers.Concrete_Types.Instant.Prefab_Provider;
using Spells.Implementations_Interfaces.Implementations;
using Spells.Spell.Interfaces;
using UnityEngine;
using Zenject;

namespace Spells.Factory
{
    public class SpellObjectsFactory : FactoryWithInstantiatorBase, ISpellObjectsFactory
    {
        public SpellObjectsFactory(IInstantiator instantiator, Transform defaultParentTransform) : base(instantiator,
            defaultParentTransform)
        {
        }

        public void Create(ISpellDataForSpellController spellData, ISpellPrefabProvider spellPrefabProvider,
            ICaster caster, Vector3 spawnPosition, Quaternion spawnRotation)
        {
            var spellController =
                InstantiatePrefabForComponent<IInitializableSpellObjectController>(spellPrefabProvider, spawnPosition,
                    spawnRotation);
            spellController.Initialize(spellData, caster, this);
        }

        public IContinuousSpellController Create(IDataForContinuousSpellController spellControllerData,
            IContinuousSpellPrefabProvider prefabProvider, ICaster caster, IReadonlyTransform castPoint)
        {
            var spellController =
                InstantiatePrefabForComponent<IInitializableContinuousSpellController>(prefabProvider,
                    castPoint.Position, castPoint.Rotation);
            spellController.Initialize(spellControllerData, caster, castPoint, this);
            return spellController;
        }

        public IInstantSpellController Create(IDataForInstantSpellController spellControllerData,
            IInstantSpellPrefabProvider prefabProvider, ICaster caster, Vector3 spawnPosition, Quaternion spawnRotation,
            IReadonlyTransform castPoint)
        {
            var spellController =
                InstantiatePrefabForComponent<IInitializableInstantSpellController>(prefabProvider, spawnPosition,
                    spawnRotation);
            spellController.Initialize(spellControllerData, caster, this, castPoint);
            return spellController;
        }
    }
}