using DG.Tweening;

namespace Puzzles.Mechanisms_Triggers.Concrete_Types.Plate.Settings
{
    public interface IPlateSettings
    {
        float AnimationDuration { get; }
        Ease AnimationEase { get; }
        float PressedScaleY { get; }
        float UnpressedScaleY { get; }
    }
}