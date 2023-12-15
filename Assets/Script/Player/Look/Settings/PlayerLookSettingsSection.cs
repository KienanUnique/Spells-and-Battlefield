using System;
using Common.Look;
using UnityEngine;

namespace Player.Look.Settings
{
    [Serializable]
    public class PlayerLookSettingsSection : LookSettingsSection, IPlayerLookSettings
    {
        [SerializeField] private float _upperLimit = -40f;
        [SerializeField] private float _bottomLimit = 70f;

        public float UpperLimit => _upperLimit;
        public float BottomLimit => _bottomLimit;
    }
}