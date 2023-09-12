using UnityEngine;

namespace Enemies.General_Settings
{
    [CreateAssetMenu(fileName = "Enemy Settings",
        menuName = ScriptableObjectsMenuDirectories.GeneralSettingsDirectory + "Enemy Settings", order = 0)]
    public class GeneralEnemySettings : ScriptableObject, IGeneralEnemySettings
    {
        [Min(1f)] [SerializeField] private float _delayInSecondsBeforeDestroy = 5f;
        [Min(1f)] [SerializeField] private float _targetSelectorUpdateCooldownInSeconds = 2f;
        [SerializeField] private AnimationClip _emptyActionAnimationClip;

        public AnimationClip EmptyActionAnimationClip => _emptyActionAnimationClip;
        public float DelayInSecondsBeforeDestroy => _delayInSecondsBeforeDestroy;
        public float TargetSelectorUpdateCooldownInSeconds => _targetSelectorUpdateCooldownInSeconds;
    }
}