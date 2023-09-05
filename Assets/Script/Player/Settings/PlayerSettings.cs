using Player.Camera_Effects.Settings;
using Player.Character.Settings;
using Player.Look.Settings;
using Player.Movement.Settings;
using Player.Spell_Manager.Settings;
using Player.Visual.Settings;
using UnityEngine;

namespace Player.Settings
{
    [CreateAssetMenu(fileName = "Player Settings",
        menuName = ScriptableObjectsMenuDirectories.GeneralSettingsDirectory + "Player Settings", order = 0)]
    public class PlayerSettings : ScriptableObject, IPlayerSettings
    {
        [SerializeField] private PlayerCameraEffectsSettingsSection _cameraEffects;
        [SerializeField] private PlayerLookSettingsSection _look;
        [SerializeField] private PlayerMovementSettingsSection _movement;
        [SerializeField] private PlayerCharacterSettingsSection _character;
        [SerializeField] private PlayerSpellManagerSettingsSection _spellManager;
        [SerializeField] private PlayerVisualSettingsSection _visual;

        public IPlayerCameraEffectsSettings CameraEffects => _cameraEffects;
        public IPlayerLookSettings Look => _look;
        public IPlayerMovementSettings Movement => _movement;
        public IPlayerCharacterSettings Character => _character;
        public IPlayerSpellManagerSettings SpellManager => _spellManager;
        public IPlayerVisualSettings Visual => _visual;
    }
}