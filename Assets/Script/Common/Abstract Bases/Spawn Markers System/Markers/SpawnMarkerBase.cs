using UnityEngine;

namespace Common.Abstract_Bases.Spawn_Markers_System.Markers
{
    public abstract class SpawnMarkerBase : MonoBehaviour, ISpawnMarker
    {
        public bool IsDisabled => !gameObject.activeInHierarchy;
        public Vector3 Position => transform.position;
        public Quaternion Rotation => transform.rotation;
    }
}