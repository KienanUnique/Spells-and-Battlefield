using UnityEngine;

namespace Enemies.General_Settings
{
    [CreateAssetMenu(fileName = "Enemy Settings",
        menuName = ScriptableObjectsMenuDirectories.GeneralSettingsDirectory + "Enemy Settings", order = 0)]
    public class GeneralEnemySettings : ScriptableObject, IGeneralEnemySettings
    {
        [Min(1f)] [SerializeField] private float _delayInSecondsBeforeDestroy = 1f;
        [SerializeField] private Vector3 _spawnSpellOffset = new Vector3(0, 3f, 0);
        [SerializeField] private AnimationClip _emptyActionAnimationClip;

        public AnimationClip EmptyActionAnimationClip => _emptyActionAnimationClip;
        public Vector3 SpawnSpellOffset => _spawnSpellOffset;
        public float DelayInSecondsBeforeDestroy => _delayInSecondsBeforeDestroy;
    }
}