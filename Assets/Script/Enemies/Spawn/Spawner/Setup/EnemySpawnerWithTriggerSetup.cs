using System.Collections.Generic;
using System.Linq;
using Common.Abstract_Bases;
using Common.Abstract_Bases.Factories.Position_Data_For_Instantiation;
using Common.Editor_Label_Text_Display;
using Enemies.Spawn.Data_For_Spawn;
using Enemies.Spawn.Factory;
using UnityEngine;
using Zenject;
using IInitializable = Common.Abstract_Bases.Initializable_MonoBehaviour.IInitializable;

namespace Enemies.Spawn.Spawner.Setup
{
    [RequireComponent(typeof(EditorLabelTextDisplay))]
    public class EnemySpawnerWithTriggerSetup : SetupMonoBehaviourBase, ITextForEditorLabelProvider
    {
        [SerializeField] private EnemyDataForSpawnMarker _objectToSpawn;
        private IEnemyFactory _enemyFactory;
        private IInitializableEnemySpawnerWithTrigger _spawner;
        private IPositionDataForInstantiation _thisPositionData;

        [Inject]
        private void GetDependencies(IEnemyFactory enemyFactory)
        {
            _enemyFactory = enemyFactory;
        }

        public string TextForEditorLabel => _objectToSpawn == null ? string.Empty : _objectToSpawn.name;

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization =>
            Enumerable.Empty<IInitializable>();

        protected override void Initialize()
        {
            _spawner.Initialize(_enemyFactory, _thisPositionData, _objectToSpawn);
        }

        protected override void Prepare()
        {
            _spawner = GetComponent<IInitializableEnemySpawnerWithTrigger>();
            _thisPositionData = new PositionDataForInstantiation(transform.position, transform.rotation);
        }
    }
}