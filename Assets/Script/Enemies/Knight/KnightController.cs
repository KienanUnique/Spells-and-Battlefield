using General_Settings_in_Scriptable_Objects;
using UnityEngine;
using Zenject;

namespace Enemies.Knight
{
    [RequireComponent(typeof(KnightCharacter))]
    public class KnightController : EnemyControllerBase
    {
        [SerializeField] private KnightVisual _knightVisual;
        [SerializeField] private BoxColliderTargetSelector _swordTargetSelector;
        protected override EnemyVisualBase EnemyVisual => _knightVisual;
        protected override IEnemySettings EnemySettings => _knightSettings;
        private KnightCharacter _knightCharacter;
        private KnightSettings _knightSettings;

        [Inject]
        private void Construct(KnightSettings knightSettings)
        {
            _knightSettings = knightSettings;
        }

        public void StartSwordAttack(Transform target)
        {
            _knightVisual.StartAttackWithSwordAnimation();
            _enemyMovement.StartMovingToTarget(target);
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