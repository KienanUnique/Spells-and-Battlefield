using System;
using System.Collections.Generic;
using Common.Readonly_Transform;
using Enemies.Loot_Dropper.Generator;
using Pickable_Items.Factory;
using Systems.Scenes_Controller.Game_Level_Loot_Unlocker;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies.Loot_Dropper
{
    public class LootDropper : ILootDropper
    {
        private const float MinimumAngleBetweenDropDirections = 20f;
        private const float PriorityDropZoneAngle = 180f;

        private readonly ILootGenerator _generator;
        private readonly IPickableItemsFactory _pickableItemsFactory;
        private readonly IReadonlyTransform _itemsSpawnPoint;
        private readonly IGameLevelLootUnlocker _gameLevelLootUnlocker;

        public LootDropper(ILootGenerator generator, IPickableItemsFactory pickableItemsFactory,
            IReadonlyTransform itemsSpawnPoint, IGameLevelLootUnlocker gameLevelLootUnlocker)
        {
            _generator = generator;
            _pickableItemsFactory = pickableItemsFactory;
            _itemsSpawnPoint = itemsSpawnPoint;
            _gameLevelLootUnlocker = gameLevelLootUnlocker;
        }

        private static IReadOnlyList<Vector3> CalculateRandomDropDirections(int count)
        {
            var dropDirections = new List<Vector3>();
            for (var i = 0; i < count; i++)
            {
                var x = Random.Range(-1f, 1f);
                var y = Random.Range(-1f, 1f);
                var z = Random.Range(-1f, 1f);
                var direction = new Vector3(x, y, z).normalized;
                dropDirections.Add(direction);
            }

            return dropDirections;
        }

        private static IReadOnlyList<Vector3> CalculateOrderedDropDirections(int count, Vector3 priorityDropDirection)
        {
            var dropDirections = new List<Vector3>();
            float angleBetweenLines;
            if (PriorityDropZoneAngle / count > MinimumAngleBetweenDropDirections)
            {
                angleBetweenLines = PriorityDropZoneAngle / count;
            }
            else
            {
                angleBetweenLines = Math.Min(360f / count, MinimumAngleBetweenDropDirections);
            }

            var currentAngle = 0f;

            for (var i = 0; i < count; i++)
            {
                var lineDirection = Quaternion.Euler(0, currentAngle, 0) * priorityDropDirection;
                dropDirections.Add(lineDirection.normalized);

                currentAngle += angleBetweenLines;
            }

            return dropDirections;
        }

        public void DropLoot(Vector3 priorityDropDirection)
        {
            var lootToDrop = _generator.GetLoot(_gameLevelLootUnlocker);
            var directions = CalculateDropDirections(lootToDrop.Count, priorityDropDirection);

            if (lootToDrop.Count != directions.Count)
            {
                throw new InvalidOperationException("Calculated directions count isn't equal loot count");
            }

            for (var i = 0; i < lootToDrop.Count; i++)
            {
                var dropDirection = directions[i];
                _pickableItemsFactory.Create(lootToDrop[i], _itemsSpawnPoint.Position, dropDirection);
            }
        }

        private IReadOnlyList<Vector3> CalculateDropDirections(int count, Vector3 priorityDropDirection)
        {
            return 360f / count < MinimumAngleBetweenDropDirections
                ? CalculateRandomDropDirections(count)
                : CalculateOrderedDropDirections(count, priorityDropDirection);
        }
    }
}