using System;
using Common.Capsule_Size_Information;
using Common.Readonly_Transform;
using UnityEngine;

namespace Enemies.Spawn.Spawn_Point_Selector
{
    public class SpawnPointSelector : ISpawnPointSelector
    {
        private const float SpawnObjectOffsetRadius = 0.5f;
        private const float AngleBetweenCheckDirectories = 360f / CountOfCheckDirections;
        private const float AngleOffsetFromForwardDirection = 30f;
        private const int MaxCountOfDetectedCollisions = 20;
        private const int CountOfCheckDirections = 12;
        private readonly Vector3 _upOffsetFromTheGround = Vector3.up * 0.2f;

        public Vector3 CalculateFreeSpawnPosition(LayerMask groundLayer, ICapsuleSizeInformation spawnObjectSize,
            float spawnAreaRadius, IReadonlyTransform spawnAreaCenterTransform)
        {
            var minimumCountOfCollisions = int.MaxValue;
            Vector3 bestPointPosition = Vector3.zero;
            RaycastHit hit;
            var overlapColliders = new Collider[MaxCountOfDetectedCollisions];

            Vector3 projectedForwardDirection = Vector3.ProjectOnPlane(spawnAreaCenterTransform.Forward, Vector3.up);
            Vector3 projectedForwardDirectionWithOffset =
                (Quaternion.AngleAxis(AngleOffsetFromForwardDirection, Vector3.up) * projectedForwardDirection)
                .normalized;

            for (var i = 0; i < CountOfCheckDirections; i++)
            {
                Vector3 rotation = Quaternion.AngleAxis(i * AngleBetweenCheckDirectories, Vector3.up) *
                                   projectedForwardDirectionWithOffset;
                Vector3 pointToCheck = spawnAreaCenterTransform.Position + rotation * spawnAreaRadius;

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