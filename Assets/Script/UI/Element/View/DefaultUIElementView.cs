using System;
using DG.Tweening;
using UI.Element.View.Settings;
using UnityEngine;

namespace UI.Element.View
{
    public class DefaultUIElementView : IUIElementView
    {
        protected readonly GameObject _cachedGameObject;
        protected readonly Transform _cachedTransform;
        private readonly IDefaultUIElementViewSettings _settings;

        public DefaultUIElementView(Transform cachedTransform, IDefaultUIElementViewSettings settings)
        {
            _cachedTransform = cachedTransform;
            _cachedGameObject = _cachedTransform.gameObject;
            _settings = settings;
        }

        public virtual void Appear()
        {
            _cachedTransform.DOKill();
            _cachedTransform.DOScale(Vector3.one, _settings.ScaleAnimationDuration)
                            .ApplyCustomSetupForUI(_cachedGameObject)
                            .SetEase(_settings.ScaleAnimationEase);
        }

        public virtual void Disappear()
        {
            Disappear(null);
        }

        public void Disappear(Action callbackOnAnimationEnd)
        {
            _cachedTransform.DOKill();
            _cachedTransform.DOScale(Vector3.zero, _settings.ScaleAnimationDuration)
                            .ApplyCustomSetupForUI(_cachedGameObject)
                            .SetEase(_settings.ScaleAnimationEase)
                            .OnComplete(() => { callbackOnAnimationEnd?.Invoke(); });
        }

        public virtual void DisappearWithoutAnimation()
        {
            _cachedTransform.localScale = Vector3.zero;
        }
    }
}