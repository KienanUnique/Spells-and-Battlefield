using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Bar
{
    public class BarController : MonoBehaviour, IBarController
    {
        [SerializeField] private Image _foreground;
        [SerializeField] private float _hpChangeDuration;

        public void UpdateValue(float newValueRatio)
        {
            this.DOKill();
            var oldValueRatio = _foreground.fillAmount;
            DOVirtual.Float(oldValueRatio, newValueRatio, _hpChangeDuration,
                currentValueRatio => _foreground.fillAmount = currentValueRatio);
        }
    }
}