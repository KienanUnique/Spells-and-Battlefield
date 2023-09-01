using System;
using Settings.Sections;
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