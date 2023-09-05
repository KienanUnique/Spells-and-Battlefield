﻿using System.Collections.Generic;
using Common.Abstract_Bases;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Enemies.Spawn.Spawner;
using UnityEngine;

namespace Puzzles.Mechanisms_Triggers.Concrete_Types.Spawned_Enemy_Death
{
    public class SpawnedEnemiesDeathMechanismTriggerSetup : SetupMonoBehaviourBase
    {
        [SerializeField] private List<EnemySpawnerWithTrigger> _triggers;
        private IInitializableSpawnedEnemiesDeathMechanismTrigger _deathMechanismTrigger;
        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization => _triggers;

        protected override void Prepare()
        {
            _deathMechanismTrigger = GetComponent<IInitializableSpawnedEnemiesDeathMechanismTrigger>();
        }

        protected override void Initialize()
        {
            _deathMechanismTrigger.Initialize(new List<IEnemyDeathTrigger>(_triggers));
        }
    }
}