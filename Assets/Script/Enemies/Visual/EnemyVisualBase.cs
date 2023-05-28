using UnityEngine;

namespace Enemies.Visual
{
    [RequireComponent(typeof(Animator))]
    public abstract class EnemyVisualBase : MonoBehaviour, IEnemyVisualBase
    {
        protected Animator _characterAnimator;
        private static readonly int IsRunningBoolHash = Animator.StringToHash("Is Running");
        private static readonly int DieTriggerHash = Animator.StringToHash("Die");

        public void UpdateMovingData(bool isRunning)
        {
            _characterAnimator.SetBool(IsRunningBoolHash, isRunning);
        }

        public void PlayDieAnimation()
        {
            _characterAnimator.SetTrigger(DieTriggerHash);
        }

        protected virtual void Awake()
        {
            _characterAnimator = GetComponent<Animator>();
        }
    }
}