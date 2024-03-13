using System.Collections.Generic;
using UI.Concrete_Scenes.In_Game.Damage_Indicator.View.Damage_Indicator_Element;
using UI.Element.View;
using UI.Element.View.Settings;
using UnityEngine;

namespace UI.Concrete_Scenes.In_Game.Damage_Indicator.View
{
    public class DamageIndicatorView : DefaultUIElementView, IDamageIndicatorView
    {
        private const float FullCircleAngle = 360f;
        private readonly IReadOnlyList<IDamageIndicatorElement> _indicatorsInClockwiseOrder;
        private readonly float _indicatorElementAngle;

        public DamageIndicatorView(Transform cachedTransform, IDefaultUIElementViewSettings settings,
            IReadOnlyList<IDamageIndicatorElement> indicatorsInClockwiseOrder) : base(cachedTransform, settings)
        {
            _indicatorsInClockwiseOrder = indicatorsInClockwiseOrder;
            _indicatorElementAngle = FullCircleAngle / _indicatorsInClockwiseOrder.Count;
        }

        public void IndicateAboutExternalDamage(float screenSpaceDamageAngle)
        {
            var indexOfElement = Mathf.RoundToInt(screenSpaceDamageAngle / _indicatorElementAngle);
            _indicatorsInClockwiseOrder[indexOfElement].AppearTemporarily();
        }

        public void IndicateAboutLocalDamage()
        {
            foreach (var damageIndicatorElement in _indicatorsInClockwiseOrder)
            {
                damageIndicatorElement.AppearTemporarily();
            }
        }
    }
}