using System.Collections.Generic;
using Player;
using UI.Concrete_Scenes.In_Game.Continuous_Effects_Panel.Indicator.Factory;
using UI.Concrete_Scenes.In_Game.Continuous_Effects_Panel.Model;
using UI.Concrete_Scenes.In_Game.Continuous_Effects_Panel.Presenter;
using UI.Element.Setup;
using UI.Element.View;
using UnityEngine;
using Zenject;
using IInitializable = Common.Abstract_Bases.Initializable_MonoBehaviour.IInitializable;

namespace UI.Concrete_Scenes.In_Game.Continuous_Effects_Panel.Setup
{
    public class PlayerContinuousEffectsPanelPresenterSetup : UIElementPresenterSetup
    {
        [SerializeField] private Transform _contentParentTransform;
        private IInitializableContinuousEffectsPanelPresenter _presenter;
        private IContinuousEffectsPanelModel _model;
        private IUIElementView _view;
        private IPlayerInformationProvider _playerInformation;
        private IPlayerInitializationStatus _playerInitializationStatus;
        private IContinuousEffectIndicatorFactory _factory;

        [Inject]
        private void GetDependencies(IPlayerInformationProvider playerInformation,
            IPlayerInitializationStatus playerInitializationStatus, IContinuousEffectIndicatorFactory factory)
        {
            _playerInformation = playerInformation;
            _playerInitializationStatus = playerInitializationStatus;
            _factory = factory;
        }

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization =>
            new List<IInitializable> {_playerInitializationStatus};

        protected override void Prepare()
        {
            _presenter = GetComponent<IInitializableContinuousEffectsPanelPresenter>();
            _view = new DefaultUIElementView(transform, DefaultUIElementViewSettings);
            _model = new ContinuousEffectsPanelModel(_factory, _contentParentTransform);
        }

        protected override void Initialize()
        {
            _presenter.Initialize(_model, _view, _playerInformation);
        }
    }
}