using System;
using Interfaces;

namespace Enemies.Trigger
{
    public interface IEnemyTargetTrigger
    {
        public event Action<IEnemyTarget> TargetDetected;
        public event Action<IEnemyTarget> TargetLost;
        public bool IsTargetInTrigger(IEnemyTarget target);
    }
}