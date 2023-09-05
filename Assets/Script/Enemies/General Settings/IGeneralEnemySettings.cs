using UnityEngine;

namespace Enemies.General_Settings
{
    public interface IGeneralEnemySettings
    {
        AnimationClip EmptyActionAnimationClip { get; }
        Vector3 SpawnSpellOffset { get; }
        float DelayInSecondsBeforeDestroy { get; }
    }
}