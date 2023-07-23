using System;
using Interfaces;

namespace Common.Mechanic_Effects.Continuous_Effect
{
    public interface IAppliedContinuousEffect
    {
        event Action<IContinuousEffect> EffectEnded;
        void Start(ICoroutineStarter coroutineStarter);
        void End();
    }
}