using Common.Interfaces;
using UnityEngine;

namespace Common.Mechanic_Effects.Continuous_Effect
{
    public interface IAppliedContinuousEffect : IAppliedContinuousEffectInformation
    {
        public void Start(ICoroutineStarter coroutineStarter, GameObject gameObjectToLink);
        public void End();
    }
}