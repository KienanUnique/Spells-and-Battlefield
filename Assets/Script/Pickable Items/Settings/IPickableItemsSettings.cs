using DG.Tweening;

namespace Pickable_Items.Settings
{
    public interface IPickableItemsSettings
    {
        float AnimationMinimumHeight { get; }
        float AnimationMaximumHeight { get; }
        float YAnimationDuration { get; }
        float RotateAnimationDuration { get; }
        float AppearScaleAnimationDuration { get; }
        float DisappearScaleAnimationDuration { get; }
        float DropForce { get; }
        Ease SizeChangeEase { get; }
        Ease RotatingEase { get; }
        Ease YMovingEase { get; }
    }
}