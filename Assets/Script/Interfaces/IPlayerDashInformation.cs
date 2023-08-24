using System;

namespace Interfaces
{
    public interface IPlayerDashInformation : IPlayerDashCooldownRatioInformation
    {
        public event Action Dashed;
        public event Action DashAiming;
    }
}