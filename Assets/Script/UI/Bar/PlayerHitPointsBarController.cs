using DG.Tweening;
using Game_Managers;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Bar
{
    public class PlayerHitPointsBarController : UIElementController
    {
        [SerializeField] private Image _foreground;
        [SerializeField] private Image _foregroundBackground;
        [SerializeField] private float _changeDuration = 0.1f;

        private ICharacterInformation _playerCharacter;

        [Inject]
        private void Construct(IPlayerInformation playerInformation)
        {
            _playerCharacter = playerInformation;
        }

        private void OnEnable()
        {
            _playerCharacter.HitPointsCountChanged += OnPlayerHitPointsCountChanged;
        }

        private void OnDisable()
        {
            _playerCharacter.HitPointsCountChanged -= OnPlayerHitPointsCountChanged;
        }

        private void OnPlayerHitPointsCountChanged(float obj) =>
            UpdateValue(_playerCharacter.HitPointCountRatio);

        private void UpdateValue(float newValueRatio)
        {
            this.DOKill();
            var oldValueRatio = _foreground.fillAmount;
            DOVirtual
                .Float(oldValueRatio, newValueRatio, _changeDuration,
                    currentValueRatio => _foreground.fillAmount = currentValueRatio).ApplyCustomSetupForUI(gameObject)
                .OnComplete(() =>
                    DOVirtual.Float(oldValueRatio, newValueRatio, _changeDuration,
                            currentValueRatio => _foregroundBackground.fillAmount = currentValueRatio)
                        .ApplyCustomSetupForUI(gameObject));
        }
    }
}