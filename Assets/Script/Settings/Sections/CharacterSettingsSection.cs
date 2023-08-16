using System;
using UnityEngine;

namespace Settings.Sections
{
    [Serializable]
    public class CharacterSettingsSection
    {
        [SerializeField] protected int _maximumCountOfHitPoints;
        public int MaximumCountOfHitPoints => _maximumCountOfHitPoints;
    }
}