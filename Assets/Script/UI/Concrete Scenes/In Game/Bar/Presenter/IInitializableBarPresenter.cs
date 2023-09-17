using System.Collections.Generic;
using Common.Abstract_Bases.Disableable;
using UI.Concrete_Scenes.In_Game.Bar.Model;
using UI.Concrete_Scenes.In_Game.Bar.View;

namespace UI.Concrete_Scenes.In_Game.Bar.Presenter
{
    public interface IInitializableBarPresenter
    {
        public void Initialize(IBarModel model, IBarView view, List<IDisableable> itemsNeedDisabling);
    }
}