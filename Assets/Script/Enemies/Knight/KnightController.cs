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

        public void StartSwordAttack(Transform target)
        {
            _knightVisual.StartAttackWithSwordAnimation();
            _enemyMovement.StartMovingToTarget(Target.MainTransform);
        }

        public void StopSwordAttack()
        {
            _knightVisual.StopAttackWithSwordAnimation();
        }

        protected override void Awake()
        {
            base.Awake();
            _knightCharacter = GetComponent<KnightCharacter>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _knightVisual.AttackWithSwordAnimationMomentStart += HandleAttackWithSwordAnimationMomentStart;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _knightVisual.AttackWithSwordAnimationMomentStart -= HandleAttackWithSwordAnimationMomentStart;
        }


        private void HandleAttackWithSwordAnimationMomentStart()
        {
            var targets = _swordTargetSelector.GetTargetsInCollider();
            targets.RemoveAll(target => target.Id == Id);
            _knightCharacter.DamageTargetsWithSwordAttack(targets);
        }
    }
}