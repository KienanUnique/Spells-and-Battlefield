using Common.Capsule_Size_Information;
using UnityEngine;

namespace Enemies.Spawn.Spawn_Point_Selector
{
    public interface ISpawnPointSelector
    {
        public Vector3 CalculateFreeSpawnPosition(LayerMask groundLayer, ICapsuleSizeInformation spawnObjectSize,
            float spawnAreaRadius, Vector3 spawnAreaCenter);
    }
}