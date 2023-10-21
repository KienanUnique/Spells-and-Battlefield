using Common.Abstract_Bases.Character;
using UI.Concrete_Scenes.In_Game.Continuous_Effects_Panel.Model;
using UI.Element.View;

namespace UI.Concrete_Scenes.In_Game.Continuous_Effects_Panel.Presenter
{
    public interface IInitializableContinuousEffectsPanelPresenter
    {
        public void Initialize(IContinuousEffectsPanelModel model, IUIElementView view,
            ICharacterInformationProvider characterInformation);
    }
}