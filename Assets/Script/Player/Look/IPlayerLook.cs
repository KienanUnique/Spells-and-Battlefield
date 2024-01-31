using UnityEngine;

namespace Player.Look
{
    public interface IPlayerLook : IReadonlyPlayerLook
    {
        public void LookInputtedWith(Vector2 mouseLookDelta);
        public void StartLookingAtPoint(Vector3 lookPoint);
        public void StopLookingAtPoint();
    }
}