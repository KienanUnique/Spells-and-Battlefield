using System.Collections.Generic;
using Common.Abstract_Bases.Disableable;
using UI.Concrete_Scenes.In_Game.Spells_Panel.Panel.Setup;
using UI.Element.Presenter;
using UI.Element.View;
using UnityEngine;

namespace UI.Concrete_Scenes.In_Game.Spells_Panel.Panel.Presenter
{
    [RequireComponent(typeof(SpellPanelSetup))]
    public class SpellPanelPresenter : UIElementPresenterBase, IInitializableSpellPanelPresenter
    {
        private IUIElementView _view;

        public void Initialize(IUIElementView view, List<IDisableable> itemsNeedDisabling)
        {
            _view = view;
            SetItemsNeedDisabling(itemsNeedDisabling);
            SetInitializedStatus();
        }

        protected override IUIElementView View => _view;
    }
}