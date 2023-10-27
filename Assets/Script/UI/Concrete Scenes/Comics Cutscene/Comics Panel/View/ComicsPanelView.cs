using System;
using DG.Tweening;
using UI.Concrete_Scenes.Comics_Cutscene.Comics_Panel.Settings;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Concrete_Scenes.Comics_Cutscene.Comics_Panel.View
{
    public class ComicsPanelView : IComicsPanelView
    {
        private readonly IComicsPanelSettings _settings;
        private readonly RectTransform _transform;
        private readonly Image _image;
        private readonly Vector2 _startAppearPosition;
        private readonly Vector2 _endAppearPosition;
        private Sequence _currentSequence;

        public ComicsPanelView(AppearMoveDirection appearMoveDirection, IComicsPanelSettings settings,
            RectTransform transform, Image image)
        {
            _settings = settings;
            _transform = transform;
            _image = image;
            _startAppearPosition = _transform.anchoredPosition;
            switch (appearMoveDirection)
            {
                case AppearMoveDirection.Top:
                    _startAppearPosition.y += _settings.AppearOffsetFromFinalPosition;
                    break;
                case AppearMoveDirection.Bottom:
                    _startAppearPosition.y -= _settings.AppearOffsetFromFinalPosition;
                    break;
                case AppearMoveDirection.Left:
                    _startAppearPosition.x -= _settings.AppearOffsetFromFinalPosition;
                    break;
                case AppearMoveDirection.Right:
                    _startAppearPosition.x += _settings.AppearOffsetFromFinalPosition;
                    break;
                case AppearMoveDirection.InPlace:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(appearMoveDirection), appearMoveDirection, null);
            }

            _endAppearPosition = _transform.anchoredPosition;
        }

        public bool IsShown { get; private set; }

        public void Appear(Action callbackOnComplete)
        {
            _currentSequence?.Kill(true);
            _currentSequence = DOTween.Sequence();
            _currentSequence.Append(_image.DOFade(1f, _settings.AppearAnimationDurationInSeconds));
            _currentSequence.Join(_transform.DOAnchorPos(_endAppearPosition, _settings.AppearAnimationDurationInSeconds)
                                            .SetEase(_settings.AppearMoveAnimationEase));
            _currentSequence.SetLink(_transform.gameObject);

            _currentSequence.OnComplete(() =>
            {
                _currentSequence = DOTween.Sequence();
                _currentSequence.AppendInterval(_settings.PanelDisplayTimeInSeconds);
                _currentSequence.SetLink(_transform.gameObject);
                _currentSequence.OnComplete(() =>
                {
                    IsShown = true;
                    callbackOnComplete?.Invoke();
                    _currentSequence = null;
                });
            });
        }

        public void Disappear()
        {
            Disappear(null);
        }

        public void Disappear(Action callbackOnComplete)
        {
            _currentSequence?.Kill(true);
            _currentSequence = DOTween.Sequence();
            _currentSequence.Append(_image.DOFade(0f, _settings.DisappearAnimationDurationInSeconds));
            _currentSequence.SetLink(_transform.gameObject);
            _currentSequence.OnComplete(() =>
            {
                DisappearWithoutAnimation();
                callbackOnComplete?.Invoke();
                _currentSequence = null;
            });
        }

        public void SkipAnimation()
        {
            _currentSequence?.Kill(true);
        }

        public void DisappearWithoutAnimation()
        {
            Color transparentColor = _image.color;
            transparentColor.a = 0f;
            _image.color = transparentColor;
            _transform.anchoredPosition = _startAppearPosition;
            IsShown = false;
        }
    }
}