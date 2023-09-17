using System.Collections.Generic;
using Common.Abstract_Bases;
using Common.Abstract_Bases.Disableable;
using UI.Concrete_Scenes.In_Game.Bar.Model;
using UI.Concrete_Scenes.In_Game.Bar.Presenter;
using UI.Concrete_Scenes.In_Game.Bar.View;
using UnityEngine;

namespace UI.Concrete_Scenes.In_Game.Bar.Setup
{
    [RequireComponent(typeof(BarPresenter))]
    public abstract class BarSetupBase : SetupMonoBehaviourBase
    {
        private List<IDisableable> _itemsNeedDisabling;
        private IInitializableBarPresenter _presenter;

        protected override void Prepare()
        {
            _itemsNeedDisabling = new List<IDisableable>();
            _presenter = GetComponent<IInitializableBarPresenter>();
        }

        protected void AddDisableable(IDisableable newDisableable)
        {
            _itemsNeedDisabling.Add(newDisableable);
        }

        protected void InitializePresenter(IBarModel model, IBarView view)
        {
            _presenter.Initialize(model, view, _itemsNeedDisabling);
        }
    }
}