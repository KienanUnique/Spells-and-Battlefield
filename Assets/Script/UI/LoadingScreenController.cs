using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LoadingScreenController : UIElementController
    {
        [SerializeField] private Image _loadingIcon;
        [SerializeField] private float _rotateAnimationDuration;

        private Transform _cashedBackgroundTransform;
        private Transform _cashedLoadingIconTransform;

        public override void Appear()
        {
            base.Appear();
            _cashedLoadingIconTransform
                .DORotate(new Vector3(0, 0, 360), _rotateAnimationDuration, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Restart)
                .ApplyCustomSetupForUI(gameObject);
        }

        public override void Disappear()
        {
            _cashedBackgroundTransform.DOKill();
            base.Disappear();
        }

        protected override void Awake()
        {
            base.Awake();
            _cashedLoadingIconTransform = _loadingIcon.transform;
        }
    }
}