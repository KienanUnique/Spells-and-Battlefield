using System;
using Interfaces;

namespace Spells.Continuous_Effect
{
    public interface IAppliedContinuousEffect
    {
        event Action<IContinuousEffect> EffectEnded;
        void Start(ICoroutineStarter coroutineStarter);
        void End();
    }
}