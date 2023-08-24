using System.Collections.Generic;
using Common.Abstract_Bases.Disableable;
using UI.Bar.Model;
using UI.Bar.View;

namespace UI.Bar.Presenter
{
    public interface IInitializableBarPresenter
    {
        public void Initialize(IBarModel model, IBarView view, List<IDisableable> itemsNeedDisabling);
    }
}