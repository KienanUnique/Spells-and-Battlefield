using UnityEngine;

namespace Enemies.General_Settings
{
    public interface IGeneralEnemySettings
    {
        AnimationClip EmptyActionAnimationClip { get; }
        float DelayInSecondsBeforeDestroy { get; }
    }
}