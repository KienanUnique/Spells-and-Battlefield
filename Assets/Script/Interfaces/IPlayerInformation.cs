using System;

namespace Interfaces
{
    public interface IPlayerInformation : ICharacterInformation, IPhysicsInformation, IInteractable
    {
        public event Action DashCooldownFinished;
        public event Action<float> DashCooldownTimerTick;
        public event Action Dashed;
        public event Action DashAiming;
    }
}