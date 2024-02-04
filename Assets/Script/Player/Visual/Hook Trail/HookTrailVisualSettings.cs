using System;
using UnityEngine;

namespace Player.Visual.Hook_Trail
{
    [Serializable]
    public class HookTrailVisualSettings : IHookTrailVisualSettings
    {
        [SerializeField] [Min(0)] private float _hookTrailSpeed = 140f;

        public float HookTrailSpeed => _hookTrailSpeed;
    }
}