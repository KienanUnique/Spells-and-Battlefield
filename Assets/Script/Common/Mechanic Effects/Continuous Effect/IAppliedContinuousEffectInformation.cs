using System;
using UnityEngine;

namespace Common.Mechanic_Effects.Continuous_Effect
{
    public interface IAppliedContinuousEffectInformation
    {
        public event Action<IContinuousEffect> EffectEnded;
        public event Action<float> RatioOfCompletedPartToEntireDurationChanged;
        public Sprite Icon { get; }
        public float CurrentRatioOfCompletedPartToEntireDuration { get; }
    }
}