using System;
using General_Settings_in_Scriptable_Objects.Sections;
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