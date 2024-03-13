using System;

namespace Player.Press_Key_Interactor
{
    public interface IPlayerAsPressKeyInteractor
    {
        public event Action CanInteractNow;
        public event Action CanNotInteractNow;

        public bool CanInteract { get; }
    }
}