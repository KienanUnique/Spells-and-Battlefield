using System;
using Interfaces.Pickers;

namespace Interfaces
{
    public interface IPlayer : ISpellInteractable, ICharacter, IInteractable, IEnemyTarget, IDroppedItemsPicker,
        IPhysicsInteractable, IDroppedEffectPicker, IDroppedSpellPicker, IMovable
    {
        public event Action DashAiming;
        public event Action Dashed;
        public event Action DashCooldownFinished;
        public event Action<float> DashCooldownTimerTick;
    }
}