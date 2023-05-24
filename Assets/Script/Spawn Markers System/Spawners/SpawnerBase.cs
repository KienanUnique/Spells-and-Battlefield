using System.Collections.Generic;
using Spawn_Markers_System.Markers;
using UnityEngine;

namespace Spawn_Markers_System.Spawners
{
    public abstract class SpawnerBase<TSpawnMarker> : MonoBehaviour
        where TSpawnMarker : ISpawnMarkerPlaceInfo
    {
        protected List<TSpawnMarker> _markers;

        protected abstract void Spawn();

        private void Awake()
        {
            _markers = new List<TSpawnMarker>();
            _markers.AddRange(GetComponentsInChildren<TSpawnMarker>());
        }

        private void Start()
        {
            Spawn();
        }
    }
}