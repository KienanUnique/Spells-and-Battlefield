using UI.Concrete_Scenes.In_Game.Continuous_Effects_Panel.Indicator.View;

namespace UI.Concrete_Scenes.In_Game.Continuous_Effects_Panel.Indicator.Setup
{
    public interface IInitializableContinuousEffectIndicatorPresenter
    {
        void Initialize(IContinuousEffectIndicatorView view);
    }
}