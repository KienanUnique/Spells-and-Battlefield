using DG.Tweening;

namespace UI.Popup_Text.Settings
{
    public interface IPopupTextSettings
    {
        Ease MovementEase { get; }
        float MoveMaximumRadius { get; }
        float MoveMinimumRadius { get; }
        float AnimationDurationInSeconds { get; }
        Ease ScaleEase { get; }
    }
}