using DG.Tweening;
using UI.Bar.View.Concrete_Types.Bar_View_With_Additional_Display_Of_Changes.Settings;
using UnityEngine.UI;

namespace UI.Bar.View.Concrete_Types.Bar_View_With_Additional_Display_Of_Changes
{
    public class BarViewWithAdditionalDisplayOfChanges : IBarView
    {
        private readonly Image _foreground;
        private readonly Image _foregroundBackground;
        private readonly IBarViewWithAdditionalDisplayOfChangesSettings _settings;
        private Sequence _sequence;

        public BarViewWithAdditionalDisplayOfChanges(Image foreground, Image foregroundBackground,
            IBarViewWithAdditionalDisplayOfChangesSettings settings)
        {
            _foreground = foreground;
            _foregroundBackground = foregroundBackground;
            _settings = settings;
            _sequence = DOTween.Sequence();
        }

        public void UpdateFillAmount(float newFillAmount)
        {
            _sequence.Kill();
            _sequence = DOTween.Sequence();
            float oldValueRatio = _foreground.fillAmount;
            _sequence.Append(DOVirtual.Float(oldValueRatio, newFillAmount, _settings.ChangeDuration,
                currentValueRatio => _foreground.fillAmount = currentValueRatio));
            _sequence.Append(DOVirtual.Float(oldValueRatio, newFillAmount, _settings.ChangeDuration,
                currentValueRatio => _foregroundBackground.fillAmount = currentValueRatio));
            _sequence.ApplyCustomSetupForUI(_foregroundBackground.gameObject);
            _sequence.ApplyCustomSetupForUI(_foreground.gameObject);
        }
    }
}