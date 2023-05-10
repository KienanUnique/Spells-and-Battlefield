using System;
using UnityEngine;

namespace General_Settings_in_Scriptable_Objects
{
    [CreateAssetMenu(fileName = "Knight Settings",
        menuName = "Spells and Battlefield/Enemy Settings/Knight Settings", order = 0)]
    public class KnightSettings : ScriptableObject, IEnemySettings
    {
        [SerializeField] private KnightCharacterSettingsSection _knightCharacterSettings;
        [SerializeField] private TargetPathfinderSettingsSection _targetPathfinderSettings;
        [SerializeField] private MovementSettingsSectionBase _movementSettings;

        public KnightCharacterSettingsSection KnightCharacterSettings => _knightCharacterSettings;
        public CharacterSettingsSection CharacterSettings => _knightCharacterSettings;
        public MovementSettingsSectionBase MovementSettings => _movementSettings;
        public TargetPathfinderSettingsSection TargetPathfinderSettingsSection => _targetPathfinderSettings;

        [Serializable]
        public class KnightCharacterSettingsSection : CharacterSettingsSection
        {
            [SerializeField] private int _attackSwordDamage = 7;

            public int AttackSwordDamage => _attackSwordDamage;
        }
    }
}