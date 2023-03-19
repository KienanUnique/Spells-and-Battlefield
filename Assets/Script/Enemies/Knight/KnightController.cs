using UnityEngine;

namespace Enemies.Knight
{
    [RequireComponent(typeof(KnightCharacter))]
    [RequireComponent(typeof(KnightCharacter))]
    public class KnightController : EnemyControllerBase
    {
        [SerializeField] private KnightVisual _knightVisual;
        [SerializeField] private BoxColliderTargetSelector _swordTargetSelector;
        protected override EnemyVisualBase EnemyVisual => _knightVisual;
        private KnightCharacter _knightCharacter;

        protected override void Awake()
        {
            base.Awake();
            _knightCharacter = GetComponent<KnightCharacter>();
        }

        private void OnEnable()
        {
            _knightVisual.AttackWithSwordAnimationMomentStartEvent += OnAttackWithSwordAnimationMomentStartEvent;
        }

        private void OnDisable()
        {
            _knightVisual.AttackWithSwordAnimationMomentStartEvent -= OnAttackWithSwordAnimationMomentStartEvent;
        }

        private void OnAttackWithSwordAnimationMomentStartEvent()
        {
            var targets = _swordTargetSelector.GetTargetsInCollider();
            targets.RemoveAll(target => target.Id == Id);
            _knightCharacter.DamageTargetsWithSwordAttack(targets);
        }

        public void StartSwordAttack(Transform target)
        {
            _knightVisual.StartAttackWithSwordAnimation();
            _enemyMovement.StartMovingWithRotatingTowardsTarget(target);
        }

        public void StopSwordAttack()
        {
            _knightVisual.StopAttackWithSwordAnimation();
            _enemyMovement.StopCurrentAction();
        }
    }
}