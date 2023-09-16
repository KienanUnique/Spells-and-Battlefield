using System.Collections.Generic;
using Common.Abstract_Bases.Factories.Position_Data_For_Instantiation;
using Common.Mechanic_Effects.Concrete_Types.Summon;
using Enemies.Spawn.Data_For_Spawn;
using Enemies.Trigger;
using UnityEngine;

namespace Enemies.Spawn.Factory
{
    public interface IEnemyFactory
    {
        public IEnemy Create(IEnemyDataForSpawning dataForSpawning, IInformationForSummon informationForSummon,
            Vector3 spawnPosition, Quaternion spawnRotation);

        public IEnemy Create(IEnemyDataForSpawning dataForSpawning, IInformationForSummon informationForSummon,
            IPositionDataForInstantiation positionDataForInstantiation);

        public IEnemy Create(IEnemyDataForSpawning dataForSpawning, List<IEnemyTargetTrigger> enemyTargetTriggers,
            Vector3 spawnPosition, Quaternion spawnRotation);

        public IEnemy Create(IEnemyDataForSpawning dataForSpawning, List<IEnemyTargetTrigger> enemyTargetTriggers,
            IPositionDataForInstantiation positionDataForInstantiation);
    }
}