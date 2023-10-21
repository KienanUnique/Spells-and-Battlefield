using UI.Concrete_Scenes.In_Game.Continuous_Effects_Panel.Indicator.Data_For_Activation;

namespace UI.Concrete_Scenes.In_Game.Continuous_Effects_Panel.Indicator.Factory
{
    public interface IContinuousEffectIndicatorFactory
    {
        public void Create(IContinuousEffectIndicatorDataForActivation dataForActivation);
    }
}