using System;
using Common.Capsule_Size_Information;
using UnityEngine;

namespace Enemies.Spawn.Spawn_Point_Selector
{
    public class SpawnPointSelector : ISpawnPointSelector
    {
        private const int CountOfCheckDirections = 12;
        private const float AngleBetweenCheckDirectories = 2 * Mathf.PI / CountOfCheckDirections;
        private readonly Vector3 _upOffsetFromTheGround = Vector3.up * 0.2f;

        public Vector3 CalculateFreeSpawnPosition(LayerMask groundLayer, ICapsuleSizeInformation spawnObjectSize,
            float spawnAreaRadius, Vector3 spawnAreaCenter)
        {
            var minimumCountOfCollisions = int.MaxValue;
            Vector3 bestPointPosition = Vector3.zero;
            RaycastHit hit;
            var overlapColliders = new Collider[] { };

            for (var i = 0; i < CountOfCheckDirections; i++)
            {
                float angle = i * AngleBetweenCheckDirectories;
                var directionToCheck = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));
                Vector3 pointToCheck = spawnAreaCenter + directionToCheck * spawnAreaRadius;

                if (!Physics.Raycast(pointToCheck, Vector3.down, out hit, Mathf.Infinity, groundLayer))
                {
                    continue;
                }

                int numCollisions = Physics.OverlapCapsuleNonAlloc(hit.point + _upOffsetFromTheGround,
                    hit.point +
                    _upOffsetFromTheGround +
                    (spawnObjectSize.Height + spawnObjectSize.Radius * 2) * Vector3.up, spawnObjectSize.Radius,
                    overlapColliders);
                Array.Clear(overlapColliders, 0, overlapColliders.Length);

                if (numCollisions == 0)
                {
                    return hit.point;
                }

                if (numCollisions < minimumCountOfCollisions)
                {
                    minimumCountOfCollisions = numCollisions;
                    bestPointPosition = hit.point;
                }
            }

            return bestPointPosition;
        }
    }
}