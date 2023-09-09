using Enemies.Character.Provider;
using Enemies.Loot_Dropper.Provider;
using Enemies.Movement.Provider;
using Enemies.Target_Pathfinder.Provider;
using UnityEngine;

namespace Enemies.Setup
{
    public interface IEnemySettings
    {
        public IEnemyCharacterProvider CharacterProvider { get; }
        public IEnemyMovementProvider MovementProvider { get; }
        public ITargetPathfinderProvider TargetPathfinderProvider { get; }
        public ILootDropperProvider LootDropperProvider  { get; }
        public AnimatorOverrideController BaseAnimatorOverrideController { get; }
    }
}