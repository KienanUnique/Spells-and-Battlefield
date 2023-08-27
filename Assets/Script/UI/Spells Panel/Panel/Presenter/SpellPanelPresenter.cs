using System.Collections.Generic;
using Common.Abstract_Bases.Disableable;
using UI.Element.Presenter;
using UI.Element.View;
using UI.Spells_Panel.Panel.Setup;
using UnityEngine;

namespace UI.Spells_Panel.Panel.Presenter
{
    [RequireComponent(typeof(SpellPanelSetup))]
    public class SpellPanelPresenter : UIElementPresenterBase, IInitializableSpellPanelPresenter
    {
        private IUIElementView _view;
        protected override IUIElementView View => _view;

        public void Initialize(IUIElementView view, List<IDisableable> itemsNeedDisabling)
        {
            _view = view;
            SetItemsNeedDisabling(itemsNeedDisabling);
            SetInitializedStatus();
        }
    }
}