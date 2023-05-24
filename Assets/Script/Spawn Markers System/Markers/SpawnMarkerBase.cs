using Common;
using UnityEngine;

namespace Spawn_Markers_System.Markers
{
    public abstract class SpawnMarkerBase<TPrefabProviderScriptableObject, TPrefabProviderInterface> : MonoBehaviour,
        ISpawnMarker<TPrefabProviderInterface>
        where TPrefabProviderScriptableObject : PrefabProviderScriptableObjectBase, TPrefabProviderInterface
        where TPrefabProviderInterface : IPrefabProvider
    {
        [SerializeField] private TPrefabProviderScriptableObject _objectToSpawn;
        public TPrefabProviderInterface ObjectToSpawn => _objectToSpawn;
        public Vector3 Position => transform.position;
        public Quaternion Rotation => transform.rotation;
    }
}