using System;
using General_Settings_in_Scriptable_Objects.Sections;
using Settings.Sections.Movement;
using UnityEngine;

namespace Settings.Enemy
{
    [CreateAssetMenu(fileName = "Knight Settings",
        menuName = ScriptableObjectsMenuDirectories.ConcreteEnemiesSettingsDirectory + "Knight Settings", order = 0)]
    public class KnightSettings : ScriptableObject
    {
        [SerializeField] private KnightCharacterSettingsSection _knightCharacterSettings;
        [SerializeField] private TargetPathfinderSettingsSection _targetPathfinderSettings;
        [SerializeField] private MovementOnGroundSettingsSection _movementSettings;

        public KnightCharacterSettingsSection KnightCharacterSettings => _knightCharacterSettings;
        public CharacterSettingsSection CharacterSettings => _knightCharacterSettings;
        public MovementOnGroundSettingsSection MovementSettings => _movementSettings;
        public TargetPathfinderSettingsSection TargetPathfinderSettingsSection => _targetPathfinderSettings;

        [Serializable]
        public class KnightCharacterSettingsSection : CharacterSettingsSection
        {
            [SerializeField] private int _attackSwordDamage = 7;

            public int AttackSwordDamage => _attackSwordDamage;
        }
    }
}