using UnityEngine;

namespace Common.Abstract_Bases.Spawn_Markers_System.Markers
{
    public abstract class SpawnMarkerBase: MonoBehaviour
    {
        public Vector3 Position => transform.position;
        public Quaternion Rotation => transform.rotation;
    }
}