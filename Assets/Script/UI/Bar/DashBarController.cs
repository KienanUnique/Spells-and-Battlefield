using DG.Tweening;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Bar
{
    public class DashBarController : UIElementController
    {
        [SerializeField] private Image _foreground;
        [SerializeField] private float _onFillAnimationDurationSeconds = 0.3f;
        [SerializeField] private float _onFillAnimationPunchStrength = 0.15f;

        private IPlayerInformationProvider _playerInformationProvider;

        [Inject]
        private void Construct(IPlayerInformationProvider playerInformationProvider)
        {
            _playerInformationProvider = playerInformationProvider;
        }
        
        private Vector3 PunchStrengthVector3 =>
            new Vector3(_onFillAnimationPunchStrength, _onFillAnimationPunchStrength, 0);

        private void OnEnable()
        {
            _playerInformationProvider.DashCooldownTimerTick += UpdateBarFill;
            _playerInformationProvider.DashCooldownFinished += PlayFullBarScaleAnimation;
        }

        private void OnDisable()
        {
            _playerInformationProvider.DashCooldownTimerTick -= UpdateBarFill;
            _playerInformationProvider.DashCooldownFinished -= PlayFullBarScaleAnimation;
        }

        private void UpdateBarFill(float valueRatio)
        {
            _foreground.fillAmount = valueRatio;
        }

        private void PlayFullBarScaleAnimation()
        {
            _cashedTransform.DOPunchScale(PunchStrengthVector3, _onFillAnimationDurationSeconds)
                .ApplyCustomSetupForUI(gameObject);
        }
    }
}