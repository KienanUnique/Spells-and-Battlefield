using DG.Tweening;
using UI.Bar.View.Concrete_Types.Filling_Bar.Settings;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Bar.View.Concrete_Types.Filling_Bar
{
    public class FillingBarView : IBarView
    {
        private readonly Transform _barTransform;
        private readonly Image _foreground;
        private readonly Vector3 _punchStrengthVector3;
        private readonly IFillingBarSettings _settings;

        public FillingBarView(Image foreground, Transform barTransform, IFillingBarSettings settings)
        {
            _foreground = foreground;
            _barTransform = barTransform;
            _settings = settings;
            _punchStrengthVector3 = new Vector3(_settings.OnFillAnimationPunchStrength,
                _settings.OnFillAnimationPunchStrength, 0);
        }

        public void UpdateFillAmount(float newFillAmount)
        {
            _foreground.fillAmount = newFillAmount;
            if (newFillAmount == 1f)
            {
                _barTransform.DOComplete();
                _barTransform.DOPunchScale(_punchStrengthVector3, _settings.OnFillAnimationDurationSeconds)
                             .ApplyCustomSetupForUI(_barTransform.gameObject);
            }
        }
    }
}