using DG.Tweening;

namespace UI.Concrete_Scenes.In_Game.Damage_Indicator.Settings
{
    public interface IDamageIndicatorElementSettings
    {
        public float AppearFadeDuration { get; }
        public Ease AppearFadeEase { get; }
        public float DisappearFadeDuration { get; }
        public Ease DisappearFadeEase { get; }
    }
}