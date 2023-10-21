using System;
using Common.Abstract_Bases.Factories.Object_Pool;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Common.Mechanic_Effects.Continuous_Effect;
using UI.Concrete_Scenes.In_Game.Continuous_Effects_Panel.Indicator.Data_For_Activation;
using UI.Concrete_Scenes.In_Game.Continuous_Effects_Panel.Indicator.Setup;
using UI.Concrete_Scenes.In_Game.Continuous_Effects_Panel.Indicator.View;

namespace UI.Concrete_Scenes.In_Game.Continuous_Effects_Panel.Indicator.Presenter
{
    public class ContinuousEffectIndicatorPresenter : InitializableMonoBehaviourBase,
        IContinuousEffectIndicatorPresenter,
        IInitializableContinuousEffectIndicatorPresenter
    {
        private IAppliedContinuousEffectInformation _currentEffectInformation;
        private IContinuousEffectIndicatorView _view;

        public void Initialize(IContinuousEffectIndicatorView view)
        {
            _view = view;
            SetInitializedStatus();
            _view.DisappearWithoutAnimation();
        }

        public event Action<IObjectPoolItem<IContinuousEffectIndicatorDataForActivation>> Deactivated;
        public bool IsUsed { get; private set; }

        public void Activate(IContinuousEffectIndicatorDataForActivation dataForActivation)
        {
            if (IsUsed)
            {
                throw new InvalidOperationException();
            }

            IsUsed = true;

            _currentEffectInformation = dataForActivation.EffectInformation;
            _view.Appear(_currentEffectInformation.Icon, dataForActivation.Parent);
            _view.UpdateRatioOfCompletedPartToEntireDuration(_currentEffectInformation
                .CurrentRatioOfCompletedPartToEntireDuration);
            SubscribeOnEffectInformationEvents();
        }

        protected override void SubscribeOnEvents()
        {
            if (IsUsed)
            {
                SubscribeOnEffectInformationEvents();
            }
        }

        protected override void UnsubscribeFromEvents()
        {
            if (IsUsed)
            {
                UnsubscribeFromEffectInformationEvents();
            }
        }

        private void SubscribeOnEffectInformationEvents()
        {
            _currentEffectInformation.RatioOfCompletedPartToEntireDurationChanged +=
                OnRatioOfCompletedPartToEntireDurationChanged;
            _currentEffectInformation.EffectEnded += OnEffectEnded;
        }

        private void UnsubscribeFromEffectInformationEvents()
        {
            _currentEffectInformation.RatioOfCompletedPartToEntireDurationChanged -=
                OnRatioOfCompletedPartToEntireDurationChanged;
            _currentEffectInformation.EffectEnded -= OnEffectEnded;
        }

        private void OnEffectEnded(IContinuousEffect continuousEffect)
        {
            UnsubscribeFromEffectInformationEvents();
            IsUsed = false;
            _view.Disappear();
            Deactivated?.Invoke(this);
        }

        private void OnRatioOfCompletedPartToEntireDurationChanged(float newRatio)
        {
            _view.UpdateRatioOfCompletedPartToEntireDuration(newRatio);
        }
    }
}