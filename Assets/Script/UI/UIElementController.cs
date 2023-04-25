using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class UIElementController : MonoBehaviour, IElementUI
    {
        [SerializeField] private float _scaleAnimationDuration;
        private Transform _cashedTransform;
        private GameObject _cashedGameObject;

        public virtual void Appear()
        {
            _cashedGameObject.SetActive(true);
            _cashedTransform.DOKill();
            _cashedTransform.DOScale(Vector3.one, _scaleAnimationDuration).SetLink(gameObject);
            ;
        }

        public virtual void Disappear()
        {
            _cashedTransform.DOKill();
            _cashedTransform.DOScale(Vector3.zero, _scaleAnimationDuration).SetLink(gameObject)
                .OnComplete(() => _cashedGameObject.SetActive(false));
        }

        protected virtual void Awake()
        {
            _cashedTransform = transform;
            _cashedGameObject = gameObject;

            _cashedTransform.localScale = Vector3.zero;
            _cashedGameObject.SetActive(false);
        }
    }
}