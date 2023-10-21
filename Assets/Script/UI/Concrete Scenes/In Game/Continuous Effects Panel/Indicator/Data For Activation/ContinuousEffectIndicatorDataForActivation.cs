using Common.Mechanic_Effects.Continuous_Effect;
using UnityEngine;

namespace UI.Concrete_Scenes.In_Game.Continuous_Effects_Panel.Indicator.Data_For_Activation
{
    public class ContinuousEffectIndicatorDataForActivation : IContinuousEffectIndicatorDataForActivation
    {
        public ContinuousEffectIndicatorDataForActivation(IAppliedContinuousEffectInformation effectInformation,
            Transform parent)
        {
            EffectInformation = effectInformation;
            Parent = parent;
        }

        public IAppliedContinuousEffectInformation EffectInformation { get; }
        public Transform Parent { get; }
    }
}