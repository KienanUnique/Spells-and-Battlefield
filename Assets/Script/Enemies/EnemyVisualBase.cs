using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Animator))]
    public abstract class EnemyVisualBase : MonoBehaviour
    {
        protected Animator _characterAnimator;
        private static readonly int IsRunningBoolHash = Animator.StringToHash("Is Running");

        public void UpdateMovingData(bool isRunning)
        {
            _characterAnimator.SetBool(IsRunningBoolHash, isRunning);
        }

        protected virtual void Awake()
        {
            _characterAnimator = GetComponent<Animator>();
        }
    }
}