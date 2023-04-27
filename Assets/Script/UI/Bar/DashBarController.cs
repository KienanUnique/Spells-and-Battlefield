using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Bar
{
    public class DashBarController : UIElementController, IBarController
    {
        [SerializeField] private Image _foreground;
        [SerializeField] private float _onFillAnimationDurationSeconds = 0.3f;
        [SerializeField] private float _onFillAnimationPunchStrength = 0.5f;

        private Vector3 PunchStrengthVector3 =>
            new Vector3(_onFillAnimationPunchStrength, _onFillAnimationPunchStrength, 0);

        public void UpdateValue(float valueRatio)
        {
            _foreground.fillAmount = valueRatio;
        }

        public void PlayFullBarScaleAnimation()
        {
            _cashedTransform.DOPunchScale(PunchStrengthVector3, _onFillAnimationDurationSeconds);
        }
    }
}