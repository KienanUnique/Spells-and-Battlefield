using System;
using Interfaces;

namespace Enemies.Target_Selector
{
    public interface IEnemyTargetSelector
    {
        event Action<IEnemyTarget> CurrentTargetChanged;
        IEnemyTarget CurrentTarget { get; }
        void SwitchToNextTarget();
    }
}