﻿using Enemies.Character.Provider;
using Enemies.Look.Settings;
using Enemies.Loot_Dropper.Provider;
using Enemies.Movement.Provider;
using Enemies.Target_Pathfinder.Provider;
using UnityEngine;

namespace Enemies.Setup.Settings
{
    public interface IEnemySettings
    {
        public IEnemyCharacterProvider CharacterProvider { get; }
        public IEnemyMovementProvider MovementProvider { get; }
        public ITargetPathfinderProvider TargetPathfinderProvider { get; }
        public ILootDropperProvider LootDropperProvider { get; }
        public IEnemyLookSettingsProvider LookSettingsProvider { get; }
        public AnimatorOverrideController BaseAnimatorOverrideController { get; }
    }
}