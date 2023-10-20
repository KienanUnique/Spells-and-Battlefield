using Common.Abstract_Bases.Character;
using UI.Concrete_Scenes.In_Game.Damage_Indicator.Model;
using UI.Concrete_Scenes.In_Game.Damage_Indicator.View;

namespace UI.Concrete_Scenes.In_Game.Damage_Indicator.Setup
{
    public interface IInitializableDamageIndicatorPresenter
    {
        public void Initialize(IDamageIndicatorModel model, IDamageIndicatorView view,
            ICharacterInformationProvider characterInformation);
    }
}