using System;

namespace Player
{
    public interface IPlayerDashCooldownRatioInformation
    {
        public event Action<float> DashCooldownRatioChanged;
        public float CurrentDashCooldownRatio { get; }
    }
}