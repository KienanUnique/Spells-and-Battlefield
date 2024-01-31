using System;
using Common.Look;
using DG.Tweening;
using UnityEngine;

namespace Player.Look.Settings
{
    [Serializable]
    public class PlayerLookSettingsSection : LookSettingsSection, IPlayerLookSettings
    {
        [SerializeField] private float _upperLimit = -40f;
        [SerializeField] private float _bottomLimit = 70f;
        [SerializeField] private float _lookAtStartAnimationDuration = 1f;
        [SerializeField] private Ease _lookAtStartAnimationEase = Ease.OutCubic;

        public float UpperLimit => _upperLimit;
        public float BottomLimit => _bottomLimit;

        public float LookAtStartAnimationDuration => _lookAtStartAnimationDuration;

        public Ease LookAtStartAnimationEase => _lookAtStartAnimationEase;
    }
}