using UnityEngine;

namespace Common.Abstract_Bases.Spawn_Markers_System.Markers
{
    public abstract class SpawnMarkerBase : MonoBehaviour, ISpawnMarker
    {
        public Vector3 SpawnPosition => transform.position;
        public Quaternion SpawnRotation => transform.rotation;
        public bool IsDisabled => !gameObject.activeInHierarchy;
    }
}