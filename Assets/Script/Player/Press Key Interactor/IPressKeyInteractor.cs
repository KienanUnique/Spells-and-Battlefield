using System;

namespace Player.Press_Key_Interactor
{
    public interface IPressKeyInteractor
    {
        public event Action CanInteract;
        public event Action CanNotInteract;
        public void TryInteract();
    }
}