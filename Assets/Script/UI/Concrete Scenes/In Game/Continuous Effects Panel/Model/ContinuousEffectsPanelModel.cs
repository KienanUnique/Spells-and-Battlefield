using Common.Mechanic_Effects.Continuous_Effect;
using UI.Concrete_Scenes.In_Game.Continuous_Effects_Panel.Indicator.Data_For_Activation;
using UI.Concrete_Scenes.In_Game.Continuous_Effects_Panel.Indicator.Factory;
using UnityEngine;

namespace UI.Concrete_Scenes.In_Game.Continuous_Effects_Panel.Model
{
    public class ContinuousEffectsPanelModel : IContinuousEffectsPanelModel
    {
        private readonly IContinuousEffectIndicatorFactory _factory;
        private readonly Transform _contentParentTransform;

        public ContinuousEffectsPanelModel(IContinuousEffectIndicatorFactory factory, Transform contentParentTransform)
        {
            _factory = factory;
            _contentParentTransform = contentParentTransform;
        }

        public void HandleNewEffect(IAppliedContinuousEffectInformation effectInformation)
        {
            _factory.Create(new ContinuousEffectIndicatorDataForActivation(effectInformation, _contentParentTransform));
        }
    }
}