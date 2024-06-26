﻿using System;
using System.Collections.Generic;

namespace Enemies.Trigger
{
    public interface IEnemyTargetTrigger
    {
        public event Action<IEnemyTarget> TargetDetected;
        public event Action<IEnemyTarget> TargetLost;
        public IReadOnlyList<IEnemyTarget> TargetsInTrigger { get; }
        public bool IsTargetInTrigger(IEnemyTarget target);
        public void ForgetTarget(IEnemyTarget targetToForget);
    }
}