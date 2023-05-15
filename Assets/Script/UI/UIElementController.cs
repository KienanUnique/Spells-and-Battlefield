using DG.Tweening;
using General_Settings_in_Scriptable_Objects;
using Settings;
using UnityEngine;
using Zenject;

namespace UI
{
    public class UIElementController : MonoBehaviour, IElementUI
    {
        private UIAnimationSettings _settings;
        protected Transform _cashedTransform;
        protected GameObject _cashedGameObject;

        [Inject]
        private void Construct(UIAnimationSettings settings)
        {
            _settings = settings;
        }

        public virtual void Appear()
        {
            _cashedGameObject.SetActive(true);
            _cashedTransform.DOKill();
            _cashedTransform.SetupAppearAnimationForUI(_settings, gameObject);
        }

        public virtual void Disappear()
        {
            _cashedTransform.DOKill();
            _cashedTransform.SetupDisappearAnimationForUI(_settings, gameObject)
                .OnComplete(() => _cashedGameObject.SetActive(false));
        }

        protected virtual void Awake()
        {
            _cashedTransform = transform;
            _cashedGameObject = gameObject;
        }
    }
}