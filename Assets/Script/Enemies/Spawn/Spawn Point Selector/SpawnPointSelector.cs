using System;
using Common.Capsule_Size_Information;
using UnityEngine;

namespace Enemies.Spawn.Spawn_Point_Selector
{
    public class SpawnPointSelector : ISpawnPointSelector
    {
        private const float SpawnObjectOffsetRadius = 0.5f;
        private const float AngleBetweenCheckDirectories = 2 * Mathf.PI / CountOfCheckDirections;
        private const int MaxCountOfDetectedCollisions = 20;
        private const int CountOfCheckDirections = 12;
        private readonly Vector3 _upOffsetFromTheGround = Vector3.up * 0.2f;

        public Vector3 CalculateFreeSpawnPosition(LayerMask groundLayer, ICapsuleSizeInformation spawnObjectSize,
            float spawnAreaRadius, Vector3 spawnAreaCenter)
        {
            var minimumCountOfCollisions = int.MaxValue;
            Vector3 bestPointPosition = Vector3.zero;
            RaycastHit hit;
            var overlapColliders = new Collider[MaxCountOfDetectedCollisions];

            for (var i = 0; i < CountOfCheckDirections; i++)
            {
                float angle = i * AngleBetweenCheckDirectories;
                var directionToCheck = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));
                Vector3 pointToCheck = spawnAreaCenter + directionToCheck * spawnAreaRadius;

                if (!Physics.Raycast(pointToCheck, Vector3.down, out hit, Mathf.Infinity, groundLayer))
                {
                    continue;
                }

                float radius = spawnObjectSize.Radius + SpawnObjectOffsetRadius;
                Vector3 startCenterSpherePoint = hit.point + _upOffsetFromTheGround + radius * Vector3.up;
                Vector3 endCenterSpherePoint = startCenterSpherePoint + (spawnObjectSize.Height + radius) * Vector3.up;

                Array.Clear(overlapColliders, 0, overlapColliders.Length);
                int numCollisions = Physics.OverlapCapsuleNonAlloc(startCenterSpherePoint, endCenterSpherePoint, radius,
                    overlapColliders, ~0, QueryTriggerInteraction.Ignore);

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