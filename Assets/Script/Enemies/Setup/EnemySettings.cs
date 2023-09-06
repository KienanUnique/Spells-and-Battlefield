using Enemies.Character.Provider;
using Enemies.Movement.Provider;
using Enemies.Target_Pathfinder.Provider;
using UnityEngine;

namespace Enemies.Setup
{
    [CreateAssetMenu(fileName = "Enemy Settings",
        menuName = ScriptableObjectsMenuDirectories.EnemiesDirectory + "Enemy Settings", order = 0)]
    public class EnemySettings : ScriptableObject, IEnemySettings
    {
        [SerializeField] private EnemyMovementProviderBase _movementProvider;
        [SerializeField] private EnemyCharacterProviderBase _characterProvider;
        [SerializeField] private TargetPathfinderProvider _targetPathfinderProvider;
        [SerializeField] private AnimatorOverrideController _baseAnimatorOverrideController;
        public IEnemyCharacterProvider CharacterProvider => _characterProvider;
        public IEnemyMovementProvider MovementProvider => _movementProvider;
        public ITargetPathfinderProvider TargetPathfinderProvider => _targetPathfinderProvider;
        public AnimatorOverrideController BaseAnimatorOverrideController => _baseAnimatorOverrideController;
    }
}