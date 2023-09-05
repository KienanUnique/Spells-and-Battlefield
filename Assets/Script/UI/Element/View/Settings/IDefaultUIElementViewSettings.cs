using DG.Tweening;

namespace UI.Element.View.Settings
{
    public interface IDefaultUIElementViewSettings
    {
        float ScaleAnimationDuration { get; }
        Ease ScaleAnimationEase { get; }
    }
}