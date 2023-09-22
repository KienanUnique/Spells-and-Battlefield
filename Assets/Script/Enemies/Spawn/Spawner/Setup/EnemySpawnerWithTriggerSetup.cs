using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Abstract_Bases;
using Common.Abstract_Bases.Factories.Position_Data_For_Instantiation;
using Common.Editor_Label_Text_Display;
using Common.Settings.Ground_Layer_Mask;
using Enemies.Spawn.Data_For_Spawn;
using Enemies.Spawn.Enemy_Target_Triggers_Setter;
using Enemies.Spawn.Factory;
using Enemies.Trigger;
using UnityEngine;
using Zenject;
using IInitializable = Common.Abstract_Bases.Initializable_MonoBehaviour.IInitializable;

namespace Enemies.Spawn.Spawner.Setup
{
    [RequireComponent(typeof(EditorLabelTextDisplay))]
    public class EnemySpawnerWithTriggerSetup : SetupMonoBehaviourBase,
        ITextForEditorLabelProvider,
        IEnemyTargetTriggersSettable
    {
        private const float MaxGroundRaycastDistance = 100f;

        private readonly ExternalDependenciesInitializationWaiter _dependenciesWaiter =
            new ExternalDependenciesInitializationWaiter(false);

        [SerializeField] private EnemyDataForSpawning _objectToSpawn;
        private IEnemyFactory _enemyFactory;
        private IInitializableEnemySpawnerWithTrigger _spawner;
        private IPositionDataForInstantiation _thisPositionData;
        private IGroundLayerMaskSetting _groundLayerMaskSetting;
        private List<IEnemyTargetTrigger> _targetTriggers;

        [Inject]
        private void GetDependencies(IEnemyFactory enemyFactory, IGroundLayerMaskSetting groundLayerMaskSetting)
        {
            _enemyFactory = enemyFactory;
            _groundLayerMaskSetting = groundLayerMaskSetting;
        }

        public string TextForEditorLabel => _objectToSpawn == null ? string.Empty : _objectToSpawn.name;

        public void SetTargetTriggers(List<IEnemyTargetTrigger> targetTriggers)
        {
            _targetTriggers = targetTriggers;
            _dependenciesWaiter.HandleExternalDependenciesInitialization();
        }

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization => new[] {_dependenciesWaiter};

        protected override void Prepare()
        {
            _spawner = GetComponent<IInitializableEnemySpawnerWithTrigger>();
            Vector3 spawnPosition =
                Physics.Raycast(transform.position, Vector3.down, out RaycastHit downRaycastHit,
                    MaxGroundRaycastDistance, _groundLayerMaskSetting.Mask, QueryTriggerInteraction.Ignore)
                    ? downRaycastHit.point
                    : transform.position;
            _thisPositionData = new PositionDataForInstantiation(spawnPosition, transform.rotation);
        }

        protected override void Initialize()
        {
            _spawner.Initialize(_enemyFactory, _targetTriggers, _thisPositionData, _objectToSpawn);
        }
    }
}