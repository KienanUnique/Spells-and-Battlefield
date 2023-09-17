using System.Collections.Generic;
using Common.Abstract_Bases.Disableable;
using UI.Element.View;

namespace UI.Concrete_Scenes.In_Game.Spells_Panel.Panel.Presenter
{
    public interface IInitializableSpellPanelPresenter
    {
        public void Initialize(IUIElementView view, List<IDisableable> itemsNeedDisabling);
    }
}