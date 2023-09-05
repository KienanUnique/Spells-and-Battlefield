using System;
using UnityEngine;

namespace Common.Settings.Sections.Character
{
    [Serializable]
    public class CharacterSettingsSection : ICharacterSettings
    {
        [SerializeField] protected int _maximumCountOfHitPoints;
        public int MaximumCountOfHitPoints => _maximumCountOfHitPoints;
    }
}