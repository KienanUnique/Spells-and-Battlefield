using DG.Tweening;

namespace UI.Concrete_Scenes.In_Game.Popup_Text.Settings
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