using System;
using Game_Managers.Time_Controller;
using Interfaces.Pickers;
using Player;

namespace Interfaces
{
    public interface IPlayer : ISpellInteractable, ICharacter, IInteractable, IEnemyTarget, IDroppedItemsPicker,
        IPhysicsInteractable, IDroppedEffectPicker, IDroppedSpellPicker, IMovable
    {
        public event Action DashCooldownFinished;
        public event Action<float> DashCooldownTimerTick;
        public void Initialize(InGameInputManager inputManager, ITimeControllerForPlayer timeController);
    }
}