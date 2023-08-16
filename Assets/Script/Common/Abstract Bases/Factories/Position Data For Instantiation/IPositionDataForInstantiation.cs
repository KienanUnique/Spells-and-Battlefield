using UnityEngine;

namespace Common.Abstract_Bases.Factories.Position_Data_For_Instantiation
{
    public interface IPositionDataForInstantiation
    {
        public Vector3 SpawnPosition { get; }
        public Quaternion SpawnRotation { get; }
    }
}