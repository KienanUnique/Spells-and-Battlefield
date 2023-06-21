using System.Collections.Generic;
using Common.Abstract_Bases.Disableable;

namespace UI.Spells_Panel.Panel.Presenter
{
    public interface IInitializableSpellPanelPresenter
    {
        public void Initialize(List<IDisableable> itemsNeedDisabling);
    }
}