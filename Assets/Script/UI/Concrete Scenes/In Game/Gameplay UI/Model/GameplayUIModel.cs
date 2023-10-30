using System.Collections.Generic;
using Common.Id_Holder;
using UI.Element;
using UI.Window.Model;

namespace UI.Concrete_Scenes.In_Game.Gameplay_UI.Model
{
    public class GameplayUIModel : UIWindowModelBase, IGameplayUIModel
    {
        private readonly IReadOnlyCollection<IUIElement> _gameplayUIElements;

        public GameplayUIModel(IIdHolder idHolder, IReadOnlyCollection<IUIElement> gameplayUIElements) : base(idHolder)
        {
            _gameplayUIElements = gameplayUIElements;
        }

        public override bool CanBeClosedByPlayer => false;

        public override void Appear()
        {
            base.Appear();
            foreach (IUIElement uiElement in _gameplayUIElements)
            {
                uiElement.Appear();
            }
        }

        public override void Disappear()
        {
            base.Disappear();
            foreach (IUIElement uiElement in _gameplayUIElements)
            {
                uiElement.Disappear();
            }
        }
    }
}