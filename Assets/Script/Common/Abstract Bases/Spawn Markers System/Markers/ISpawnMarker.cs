using UnityEngine;

namespace Common.Abstract_Bases.Spawn_Markers_System.Markers
{
    public interface ISpawnMarker
    {
        public bool IsDisabled { get; }
        public Vector3 Position { get; }
        public Quaternion Rotation { get; }
    }
}