using System;
using System.Collections.Generic;
using Common.Readonly_Transform;
using Enemies.Loot_Dropper.Generator;
using Pickable_Items.Data_For_Creating;
using Pickable_Items.Factory;
using Systems.Scene_Switcher.Current_Game_Level_Information;
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
                float x = Random.Range(-1f, 1f);
                float y = Random.Range(-1f, 1f);
                float z = Random.Range(-1f, 1f);
                Vector3 direction = new Vector3(x, y, z).normalized;
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
                Vector3 lineDirection = Quaternion.Euler(0, currentAngle, 0) * priorityDropDirection;
                dropDirections.Add(lineDirection.normalized);

                currentAngle += angleBetweenLines;
            }

            return dropDirections;
        }

        public void DropLoot(Vector3 priorityDropDirection)
        {
            IReadOnlyList<IPickableItemDataForCreating> lootToDrop = _generator.GetLoot(_gameLevelLootUnlocker);
            IReadOnlyList<Vector3> directions = CalculateDropDirections(lootToDrop.Count, priorityDropDirection);

            if (lootToDrop.Count != directions.Count)
            {
                throw new InvalidOperationException("Calculated directions count isn't equal loot count");
            }

            for (var i = 0; i < lootToDrop.Count; i++)
            {
                Vector3 dropDirection = directions[i];
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