using System;
using UnityEngine;

namespace Common.Abstract_Bases.Factories.Position_Data_For_Instantiation
{
    [Serializable]
    public class SerializablePositionDataForInstantiation : IPositionDataForInstantiation
    {
        [SerializeField] private Transform _defaultSpawnPosition;
        public Vector3 SpawnPosition => _defaultSpawnPosition.position;
        public Quaternion SpawnRotation => _defaultSpawnPosition.rotation;
    }
}