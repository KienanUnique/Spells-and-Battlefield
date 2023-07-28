using Enemies.Character.Provider;
using Enemies.Movement.Provider;
using General_Settings_in_Scriptable_Objects.Sections;
using UnityEngine;

namespace Enemies.Setup
{
    public interface IEnemySettings
    {
        EnemyCharacterProviderBase CharacterProvider { get; }
        EnemyMovementProviderBase MovementProvider { get; }
        AnimatorOverrideController BaseAnimatorOverrideController { get; }
    }
}