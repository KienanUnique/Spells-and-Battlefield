using System;
using UnityEngine;

namespace Player
{
    public interface IPlayerInput
    {
        event Action JumpInputted;
        event Action StartDashAimingInputted;
        event Action DashInputted;
        event Action UseSpellInputted;
        event Action<Vector2> MoveInputted;
        event Action<Vector2> LookInputted;
    }
}