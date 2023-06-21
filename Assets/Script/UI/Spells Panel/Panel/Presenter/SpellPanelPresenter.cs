using System.Collections.Generic;
using Common.Abstract_Bases.Disableable;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using UI.Spells_Panel.Panel.Setup;
using UnityEngine;

namespace UI.Spells_Panel.Panel.Presenter
{
    [RequireComponent(typeof(SpellPanelSetup))]
    public class SpellPanelPresenter : InitializableMonoBehaviourBase, IInitializableSpellPanelPresenter
    {
        public void Initialize(List<IDisableable> itemsNeedDisabling)
        {
            SetItemsNeedDisabling(itemsNeedDisabling);
            SetInitializedStatus();
        }

        protected override void SubscribeOnEvents()
        {
        }

        protected override void UnsubscribeFromEvents()
        {
        }
    }
}