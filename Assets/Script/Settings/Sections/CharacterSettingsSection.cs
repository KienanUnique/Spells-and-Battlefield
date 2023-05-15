using System;
using UnityEngine;

namespace General_Settings_in_Scriptable_Objects.Sections
{
    [Serializable]
    public class CharacterSettingsSection
    {
        [SerializeField] protected float _maximumCountOfHitPoints;
        public float MaximumCountOfHitPoints => _maximumCountOfHitPoints;
    }
}