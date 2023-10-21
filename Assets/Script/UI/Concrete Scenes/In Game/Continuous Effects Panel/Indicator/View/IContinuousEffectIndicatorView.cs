using UnityEngine;

namespace UI.Concrete_Scenes.In_Game.Continuous_Effects_Panel.Indicator.View
{
    public interface IContinuousEffectIndicatorView
    {
        public void Appear(Sprite icon, Transform parent);
        public void UpdateRatioOfCompletedPartToEntireDuration(float newRatio);
        public void Disappear();
        public void DisappearWithoutAnimation();
    }
}