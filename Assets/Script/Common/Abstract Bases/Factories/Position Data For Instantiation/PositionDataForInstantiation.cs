using UnityEngine;

namespace Common.Abstract_Bases.Factories.Position_Data_For_Instantiation
{
    public class PositionDataForInstantiation : IPositionDataForInstantiation
    {
        public PositionDataForInstantiation(Vector3 spawnPosition, Quaternion spawnRotation)
        {
            SpawnPosition = spawnPosition;
            SpawnRotation = spawnRotation;
        }

        public PositionDataForInstantiation()
        {
            SpawnPosition = Vector3.zero;
            SpawnRotation = Quaternion.identity;
        }

        public Vector3 SpawnPosition { get; }
        public Quaternion SpawnRotation { get; }
    }
}