using Common.Interfaces;
using Common.Mechanic_Effects.Scriptable_Objects;
using Common.Mechanic_Effects.Source;
using Enemies.Spawn.Data_For_Spawn;
using Enemies.Spawn.Spawn_Point_Selector;
using Spells;
using UnityEngine;

namespace Common.Mechanic_Effects.Concrete_Types.Summon
{
    [CreateAssetMenu(fileName = "Spawn Enemy Mechanic",
        menuName = ScriptableObjectsMenuDirectories.MechanicsDirectory + "Spawn Enemy Mechanic", order = 0)]
    public class SpawnEnemyMechanic : MechanicEffectScriptableObject
    {
        [SerializeField] private EnemyDataForSpawning _dataForSpawning;
        [SerializeField] private float _spawnAreaRadius = 6f;

        public override IMechanicEffect GetImplementationObject()
        {
            return new SpawnEnemyMechanicImplementation(_dataForSpawning, new SpawnPointSelector(), _spawnAreaRadius);
        }

        private class SpawnEnemyMechanicImplementation : InstantMechanicEffectImplementationBase
        {
            private readonly IEnemyDataForSpawning _dataForSpawning;
            private readonly ISpawnPointSelector _spawnPointSelector;
            private readonly float _spawnAreaRadius;

            public SpawnEnemyMechanicImplementation(IEnemyDataForSpawning dataForSpawning,
                ISpawnPointSelector spawnPointSelector, float spawnAreaRadius)
            {
                _dataForSpawning = dataForSpawning;
                _spawnPointSelector = spawnPointSelector;
                _spawnAreaRadius = spawnAreaRadius;
            }

            public override void ApplyEffectToTarget(IInteractable target, IEffectSourceInformation sourceInformation)
            {
                if (!target.TryGetComponent(out ICaster targetAsCaster))
                {
                    return;
                }

                Vector3 spawnPoint = _spawnPointSelector.CalculateFreeSpawnPosition(
                    targetAsCaster.ToolsForSummon.GroundLayerMaskSetting.Mask,
                    _dataForSpawning.PrefabProvider.SizeInformation, _spawnAreaRadius,
                    targetAsCaster.UpperPointForSummonedEnemiesPositionCalculating);
                targetAsCaster.ToolsForSummon.Factory.Create(_dataForSpawning, targetAsCaster.InformationForSummon,
                    spawnPoint, targetAsCaster.MainTransform.Rotation);
            }
        }
    }
}