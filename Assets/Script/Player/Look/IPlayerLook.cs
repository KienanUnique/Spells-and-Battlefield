using Common;
using UnityEngine;

namespace Player.Look
{
    public interface IPlayerLook : IReadonlyLook
    {
        public void LookInputtedWith(Vector2 mouseLookDelta);
    }
}