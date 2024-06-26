﻿using Common.Abstract_Bases.Visual.Settings;
using Factions;
using Player.Camera_Effects.Settings;
using Player.Character.Settings;
using Player.Look.Settings;
using Player.Movement.Settings;
using Player.Spell_Manager.Settings;
using Player.Visual.Hook_Trail;
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
        [SerializeField] private VisualSettingsSection _visual;
        [SerializeField] private FactionScriptableObject _faction;
        [SerializeField] private HookTrailVisualSettings _hookerVisualSettings;

        public IPlayerCameraEffectsSettings CameraEffects => _cameraEffects;
        public IPlayerLookSettings Look => _look;
        public IPlayerMovementSettings Movement => _movement;
        public IPlayerCharacterSettings Character => _character;
        public IPlayerSpellManagerSettings SpellManager => _spellManager;
        public IVisualSettings Visual => _visual;
        public IFaction Faction => _faction;
        public IHookTrailVisualSettings HookerVisualSettings => _hookerVisualSettings;
    }
}