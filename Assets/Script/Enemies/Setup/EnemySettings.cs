using Enemies.Character.Provider;
using Enemies.Movement.Provider;
using UnityEngine;

namespace Enemies.Setup
{
    [CreateAssetMenu(fileName = "Enemy Settings",
        menuName = ScriptableObjectsMenuDirectories.EnemiesDirectory +
                   "Enemy Settings", order = 0)]
    public class EnemySettings : ScriptableObject, IEnemySettings
    {
        [SerializeField] private EnemyMovementProviderBase _movementProvider;
        [SerializeField] private EnemyCharacterProviderBase _characterProvider;
        [SerializeField] private AnimatorOverrideController _baseAnimatorOverrideController;
        public EnemyCharacterProviderBase CharacterProvider => _characterProvider;
        public EnemyMovementProviderBase MovementProvider => _movementProvider;
        public AnimatorOverrideController BaseAnimatorOverrideController => _baseAnimatorOverrideController;
    }
}