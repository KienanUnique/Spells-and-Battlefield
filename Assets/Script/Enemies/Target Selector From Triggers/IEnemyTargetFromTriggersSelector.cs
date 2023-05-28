using System;
using Interfaces;

namespace Enemies.Target_Selector_From_Triggers
{
    public interface IEnemyTargetFromTriggersSelector
    {
        public event Action<IEnemyTarget> CurrentTargetChanged;
        public IEnemyTarget CurrentTarget { get; }
        public void SwitchToNextTarget();
    }
}