using Common.Abstract_Bases.Factories.Object_Pool;
using UI.Concrete_Scenes.In_Game.Continuous_Effects_Panel.Indicator.Data_For_Activation;

namespace UI.Concrete_Scenes.In_Game.Continuous_Effects_Panel.Indicator.Presenter
{
    public interface IContinuousEffectIndicatorPresenter : IObjectPoolItem<IContinuousEffectIndicatorDataForActivation>
    {
    }
}