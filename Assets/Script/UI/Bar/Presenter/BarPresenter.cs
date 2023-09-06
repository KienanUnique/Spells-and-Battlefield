using System.Collections.Generic;
using Common.Abstract_Bases.Disableable;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using UI.Bar.Model;
using UI.Bar.View;

namespace UI.Bar.Presenter
{
    public class BarPresenter : InitializableMonoBehaviourBase, IInitializableBarPresenter
    {
        private IBarModel _model;
        private IBarView _view;

        public void Initialize(IBarModel model, IBarView view, List<IDisableable> itemsNeedDisabling)
        {
            _model = model;
            _view = view;
            SetItemsNeedDisabling(itemsNeedDisabling);
            UpdateFillAmount(_model.CurrentFillAmount);
            SetInitializedStatus();
        }

        protected override void SubscribeOnEvents()
        {
            if (_model != null)
            {
                _model.FillAmountChanged += UpdateFillAmount;
            }
        }

        protected override void UnsubscribeFromEvents()
        {
            if (_model != null)
            {
                _model.FillAmountChanged -= UpdateFillAmount;
            }
        }

        private void UpdateFillAmount(float newFillAmount)
        {
            _view.UpdateFillAmount(newFillAmount);
        }
    }
}