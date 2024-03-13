using Common.Abstract_Bases.Character;
using Common.Mechanic_Effects.Continuous_Effect;
using UI.Concrete_Scenes.In_Game.Continuous_Effects_Panel.Model;
using UI.Element.Presenter;
using UI.Element.View;

namespace UI.Concrete_Scenes.In_Game.Continuous_Effects_Panel.Presenter
{
    public class ContinuousEffectsPanelPresenter : UIElementPresenterBase, IInitializableContinuousEffectsPanelPresenter
    {
        private ICharacterInformationProvider _characterInformation;
        private IContinuousEffectsPanelModel _model;
        private IUIElementView _view;

        public void Initialize(IContinuousEffectsPanelModel model, IUIElementView view,
            ICharacterInformationProvider characterInformation)
        {
            _model = model;
            _view = view;
            _characterInformation = characterInformation;
            SetInitializedStatus();
            foreach (var effect in _characterInformation.CurrentContinuousEffects)
            {
                OnContinuousEffectAdded(effect);
            }
        }

        protected override IUIElementView View => _view;

        protected override void SubscribeOnEvents()
        {
            base.SubscribeOnEvents();
            _characterInformation.ContinuousEffectAdded += OnContinuousEffectAdded;
        }

        protected override void UnsubscribeFromEvents()
        {
            base.UnsubscribeFromEvents();
            _characterInformation.ContinuousEffectAdded -= OnContinuousEffectAdded;
        }

        private void OnContinuousEffectAdded(IAppliedContinuousEffectInformation effectInformation)
        {
            _model.HandleNewEffect(effectInformation);
        }
    }
}