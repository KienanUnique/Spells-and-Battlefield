using System.Collections.Generic;
using Common.Abstract_Bases.Factories.Position_Data_For_Instantiation;
using Enemies.Spawn.Data_For_Spawn;
using Enemies.Trigger;
using Interfaces;
using UnityEngine;

namespace Enemies.Spawn.Factory
{
    public interface IEnemyFactory
    {
        public IEnemy Create(IEnemyDataForSpawning dataForSpawning, List<IEnemyTargetTrigger> enemyTargetTriggers,
            Vector3 spawnPosition, Quaternion spawnRotation);

        public IEnemy Create(IEnemyDataForSpawning dataForSpawning, List<IEnemyTargetTrigger> enemyTargetTriggers,
            IPositionDataForInstantiation positionDataForInstantiation);
    }
}