using System;
using Enemies.Look_Point_Calculator;
using Interfaces;

namespace Enemies.State_Machine
{
    public interface IEnemyStateMachineAI
    {
        public event Action<ILookPointCalculator> NeedChangeLookPointCalculator;
        public void StartStateMachine();
        public void StopStateMachine();
    }
}