using DG.Tweening;
using Game_Managers;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Bar
{
    public class DashBarController : UIElementController
    {
        [SerializeField] private Image _foreground;
        [SerializeField] private float _onFillAnimationDurationSeconds = 0.3f;
        [SerializeField] private float _onFillAnimationPunchStrength = 0.15f;

        private IPlayer _player;

        private Vector3 PunchStrengthVector3 =>
            new Vector3(_onFillAnimationPunchStrength, _onFillAnimationPunchStrength, 0);

        protected override void Awake()
        {
            base.Awake();
            _player = PlayerProvider.Instance.Player;
        }

        private void OnEnable()
        {
            _player.DashCooldownTimerTick += UpdateBarFill;
            _player.DashCooldownFinished += PlayFullBarScaleAnimation;
        }

        private void OnDisable()
        {
            _player.DashCooldownTimerTick -= UpdateBarFill;
            _player.DashCooldownFinished -= PlayFullBarScaleAnimation;
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