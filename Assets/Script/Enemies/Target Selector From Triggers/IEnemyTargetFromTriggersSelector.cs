using System;
using Interfaces;

namespace Enemies.Target_Selector_From_Triggers
{
    public interface IEnemyTargetFromTriggersSelector : IReadonlyEnemyTargetFromTriggersSelector
    {
        public void SwitchToNextTarget();
    }

    public interface IReadonlyEnemyTargetFromTriggersSelector
    {
        public event Action<IEnemyTarget> CurrentTargetChanged;
        public IEnemyTarget CurrentTarget { get; }
    }
}