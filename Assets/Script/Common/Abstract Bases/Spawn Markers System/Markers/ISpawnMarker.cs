﻿using UnityEngine;

namespace Common.Abstract_Bases.Spawn_Markers_System.Markers
{
    public interface ISpawnMarker<out TPrefabProvider> : ISpawnMarkerPlaceInfo where TPrefabProvider : IPrefabProvider
    {
        public TPrefabProvider ObjectToSpawn { get; }
    }

    public interface ISpawnMarkerPlaceInfo
    {
        public Vector3 Position { get; }
        public Quaternion Rotation { get; }
    }
}