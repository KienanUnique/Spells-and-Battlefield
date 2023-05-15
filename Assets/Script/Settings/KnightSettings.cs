using System;
using General_Settings_in_Scriptable_Objects;
using General_Settings_in_Scriptable_Objects.Sections;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "Knight Settings",
        menuName = ScriptableObjectsMenuDirectories.ConcreteEnemiesSettingsDirectory + "Knight Settings", order = 0)]
    public class KnightSettings : ScriptableObject, IEnemySettings
    {
        [SerializeField] private KnightCharacterSettingsSection _knightCharacterSettings;
        [SerializeField] private TargetPathfinderSettingsSection _targetPathfinderSettings;
        [SerializeField] private MovementSettingsSection _movementSettings;

        public KnightCharacterSettingsSection KnightCharacterSettings => _knightCharacterSettings;
        public CharacterSettingsSection CharacterSettings => _knightCharacterSettings;
        public MovementSettingsSection MovementSettings => _movementSettings;
        public TargetPathfinderSettingsSection TargetPathfinderSettingsSection => _targetPathfinderSettings;

        [Serializable]
        public class KnightCharacterSettingsSection : CharacterSettingsSection
        {
            [SerializeField] private int _attackSwordDamage = 7;

            public int AttackSwordDamage => _attackSwordDamage;
        }
    }
}