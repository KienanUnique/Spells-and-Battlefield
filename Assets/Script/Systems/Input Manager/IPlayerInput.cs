﻿using System;
using Spells.Implementations_Interfaces.Implementations;
using UnityEngine;

namespace Systems.Input_Manager
{
    public interface IPlayerInput
    {
        public event Action JumpInputted;
        public event Action StartDashAimingInputted;
        public event Action DashInputted;
        public event Action UseSpellInputted;
        public event Action<Vector2> MoveInputted;
        public event Action<Vector2> LookInputted;
        public event Action<ISpellType> SelectSpellType;
    }
}