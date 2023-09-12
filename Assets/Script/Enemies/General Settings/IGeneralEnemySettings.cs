using UnityEngine;

namespace Enemies.General_Settings
{
    public interface IGeneralEnemySettings
    {
        public AnimationClip EmptyActionAnimationClip { get; }
        public float DelayInSecondsBeforeDestroy { get; }
        public float TargetSelectorUpdateCooldownInSeconds { get; }
    }
}