using System;
using Common.Readonly_Transform;
using Player.Spell_Manager;

namespace Interfaces
{
    public interface IPlayerInformationProvider : ICharacterInformationProvider, IPhysicsInformation, IPlayerSpellsManagerInformation
    {
        public event Action DashCooldownFinished;
        public event Action<float> DashCooldownTimerTick;
        public event Action Dashed;
        public event Action DashAiming;
        public IReadonlyTransform CameraTransform { get; }
    }
}