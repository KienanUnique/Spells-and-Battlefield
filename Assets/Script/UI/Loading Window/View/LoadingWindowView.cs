using DG.Tweening;
using UI.Element.View;
using UI.Element.View.Settings;
using UnityEngine;

namespace UI.Loading_Window.View
{
    public class LoadingWindowView : DefaultUIElementView, ILoadingWindowView
    {
        private readonly Transform _loadingIcon;
        private readonly ILoadingWindowSettings _loadingWindowSettings;

        public LoadingWindowView(Transform cachedTransform, IDefaultUIElementViewSettings settings,
            Transform loadingIcon, ILoadingWindowSettings loadingWindowSettings) : base(cachedTransform, settings)
        {
            _loadingIcon = loadingIcon;
            _loadingWindowSettings = loadingWindowSettings;
        }

        public override void Appear()
        {
            base.Appear();
            _loadingIcon
                .DORotate(new Vector3(0, 0, -360), _loadingWindowSettings.RotateAnimationDurationSpeed,
                    RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Restart)
                .ApplyCustomSetupForUI(_loadingIcon.gameObject);
        }

        public override void Disappear()
        {
            _loadingIcon.DOKill();
            base.Disappear();
        }
    }
}