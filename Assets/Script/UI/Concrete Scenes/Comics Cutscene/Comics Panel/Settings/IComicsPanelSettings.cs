using DG.Tweening;

namespace UI.Concrete_Scenes.Comics_Cutscene.Comics_Panel.Settings
{
    public interface IComicsPanelSettings
    {
        public float AppearAnimationDurationInSeconds { get; }
        public float DisappearAnimationDurationInSeconds { get; }
        public float AppearOffsetFromFinalPosition { get; }
        public Ease AppearMoveAnimationEase { get; }
        public float DelayBeforeAppear { get; }
    }
}