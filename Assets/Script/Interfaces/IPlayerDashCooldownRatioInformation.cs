using System;

namespace Interfaces
{
    public interface IPlayerDashCooldownRatioInformation
    {
        public event Action<float> DashCooldownRatioChanged;
        public float CurrentDashCooldownRatio { get; }
    }
}