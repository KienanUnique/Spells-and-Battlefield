using System;
using Enemies.Look_Point_Calculator;

namespace Enemies.State_Machine.States
{
    public interface IStateEnemyAI
    {
        public event Action<IStateEnemyAI> NeedToSwitchToNextState;
        public event Action<ILookPointCalculator> NeedChangeLookPointCalculator;
        public int StateID { get; }
        public ILookPointCalculator LookPointCalculator { get; }
        public void Enter();
        public void Exit();
        public void ExitSafely(IStateEnemyAI nextState);
    }
}