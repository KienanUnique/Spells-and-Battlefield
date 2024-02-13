using System.Collections.Generic;
using Player;
using Player.Press_Key_Interactor;
using UI.Concrete_Scenes.In_Game.Interact_Key_Popup.Presenter;
using UI.Element.Setup;
using UI.Element.View;
using UnityEngine;
using Zenject;
using IInitializable = Common.Abstract_Bases.Initializable_MonoBehaviour.IInitializable;

namespace UI.Concrete_Scenes.In_Game.Interact_Key_Popup.Setup
{
    public class InteractKeyPopupSetup : UIElementPresenterSetup
    {
        [SerializeField] private Transform _thisTransform;
        private IInitializableInteractKeyPopupPresenter _presenter;
        private IUIElementView _view;
        private IPlayerAsPressKeyInteractor _playerAsPressKeyInteractor;
        private IPlayerInitializationStatus _playerInitializationStatus;

        [Inject]
        private void GetDependencies(IPlayerInitializationStatus playerInitializationStatus,
            IPlayerAsPressKeyInteractor playerAsPressKeyInteractor)
        {
            _playerInitializationStatus = playerInitializationStatus;
            _playerAsPressKeyInteractor = playerAsPressKeyInteractor;
        }

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization =>
            new[] {_playerInitializationStatus};

        protected override void Prepare()
        {
            _view = new DefaultUIElementView(_thisTransform, DefaultUIElementViewSettings);
            _presenter = GetComponent<IInitializableInteractKeyPopupPresenter>();
        }

        protected override void Initialize()
        {
            _presenter.Initialize(_view, _playerAsPressKeyInteractor);
        }
    }
}