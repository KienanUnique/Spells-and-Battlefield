﻿using Enemies.Character.Provider;
using Enemies.Look.Settings;
using Enemies.Loot_Dropper.Provider;
using Enemies.Movement.Provider;
using Enemies.Target_Pathfinder.Provider;
using UnityEngine;

namespace Enemies.Setup.Settings
{
    [CreateAssetMenu(fileName = "Enemy Settings",
        menuName = ScriptableObjectsMenuDirectories.EnemiesDirectory + "Enemy Settings", order = 0)]
    public class EnemySettings : ScriptableObject, IEnemySettings
    {
        [SerializeField] private EnemyMovementProviderBase _movementProvider;
        [SerializeField] private EnemyCharacterProviderBase _characterProvider;
        [SerializeField] private TargetPathfinderProvider _targetPathfinderProvider;
        [SerializeField] private LootDropperProvider _lootDropperProvider;
        [SerializeField] private EnemyLookSettingsProvider _lookSettingsProvider;
        [SerializeField] private AnimatorOverrideController _baseAnimatorOverrideController;

        public IEnemyCharacterProvider CharacterProvider => _characterProvider;
        public IEnemyMovementProvider MovementProvider => _movementProvider;
        public ITargetPathfinderProvider TargetPathfinderProvider => _targetPathfinderProvider;
        public ILootDropperProvider LootDropperProvider => _lootDropperProvider;
        public IEnemyLookSettingsProvider LookSettingsProvider => _lookSettingsProvider;
        public AnimatorOverrideController BaseAnimatorOverrideController => _baseAnimatorOverrideController;
    }
}