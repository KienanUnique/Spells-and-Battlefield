using DG.Tweening;

namespace Puzzles.Mechanisms.Moving_Platforms.Settings
{
    public interface IMovingPlatformsSettings
    {
        Ease MovementEase { get; }
        PathType MovementPathType { get; }
    }
}