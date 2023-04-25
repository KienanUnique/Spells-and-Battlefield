using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Bar
{
    public class BarController : UIElementController, IBarController
    {
        [SerializeField] private Image _foreground;
        [SerializeField] private float _changeDuration = 0.1f;

        public void UpdateValue(float newValueRatio)
        {
            this.DOKill();
            var oldValueRatio = _foreground.fillAmount;
            DOVirtual.Float(oldValueRatio, newValueRatio, _changeDuration,
                currentValueRatio => _foreground.fillAmount = currentValueRatio).SetLink(gameObject);
        }
    }
}