using DG.Tweening;
using UI.Element.View.Settings;
using UnityEngine;

namespace UI.Element.View
{
    public class DefaultUIElementView : IUIElementView
    {
        private readonly GameObject _cachedGameObject;
        private readonly Transform _cachedTransform;
        private readonly IDefaultUIElementViewSettings _settings;

        public DefaultUIElementView(Transform cachedTransform, IDefaultUIElementViewSettings settings)
        {
            _cachedTransform = cachedTransform;
            _cachedGameObject = _cachedTransform.gameObject;
            _settings = settings;
            Disappear();
        }

        public virtual void Appear()
        {
            _cachedGameObject.SetActive(true);
            _cachedTransform.DOKill();
            _cachedTransform.DOScale(Vector3.one, _settings.ScaleAnimationDuration)
                            .ApplyCustomSetupForUI(_cachedGameObject)
                            .SetEase(_settings.ScaleAnimationEase);
        }

        public virtual void Disappear()
        {
            _cachedTransform.DOKill();
            _cachedTransform.DOScale(Vector3.zero, _settings.ScaleAnimationDuration)
                            .ApplyCustomSetupForUI(_cachedGameObject)
                            .SetEase(_settings.ScaleAnimationEase)
                            .OnComplete(() => _cachedGameObject.SetActive(false));
        }
    }
}