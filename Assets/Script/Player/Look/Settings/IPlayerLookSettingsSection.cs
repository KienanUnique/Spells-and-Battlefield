using Common.Look;
using DG.Tweening;

namespace Player.Look.Settings
{
    public interface IPlayerLookSettings : ILookSettings
    {
        public float UpperLimit { get; }
        public float BottomLimit { get; }
        float LookAtStartAnimationDuration { get; }
        Ease LookAtStartAnimationEase { get; }
    }
}