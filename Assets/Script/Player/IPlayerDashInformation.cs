using System;

namespace Player
{
    public interface IPlayerDashInformation : IPlayerDashCooldownRatioInformation
    {
        public event Action Dashed;
        public event Action DashAiming;
        public event Action DashAimingCanceled;
    }
}