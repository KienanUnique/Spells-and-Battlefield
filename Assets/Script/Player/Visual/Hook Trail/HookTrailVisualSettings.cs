using System;
using DG.Tweening;
using UnityEngine;

namespace Player.Visual.Hook_Trail
{
    [Serializable]
    public class HookTrailVisualSettings : IHookTrailVisualSettings
    {
        [SerializeField] [Min(0)] private float _hookTrailSpeed = 10f;
        [SerializeField] private Ease _hookTrailEase = Ease.OutCubic;

        public float HookTrailSpeed => _hookTrailSpeed;

        public Ease HookTrailEase => _hookTrailEase;
    }
}