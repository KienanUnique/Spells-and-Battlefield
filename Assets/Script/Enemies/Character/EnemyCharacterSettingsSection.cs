using System;
using Common.Settings.Sections;
using Common.Settings.Sections.Character;
using UnityEngine;

namespace Enemies.Character
{
    [Serializable]
    public class EnemyCharacterSettingsSection : CharacterSettingsSection
    {
        [SerializeField] private string _name;

        public string Name => _name;
    }
}