using DG.Tweening;

namespace UI.Element.View.Settings
{
    public interface IDefaultUIElementViewSettings
    {
        float ScaleAnimationDuration { get; }
        Ease AppearScaleAnimationEase { get; }
        Ease DisappearScaleAnimationEase { get; }
    }
}