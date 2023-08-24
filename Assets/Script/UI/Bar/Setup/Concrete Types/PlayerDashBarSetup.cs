using System.Collections.Generic;
using Interfaces;
using Player;
using UI.Bar.Model.Concrete_Types;
using UI.Bar.View;
using UI.Bar.View.Concrete_Types.Filling_Bar;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using IInitializable = Common.Abstract_Bases.Initializable_MonoBehaviour.IInitializable;

namespace UI.Bar.Setup.Concrete_Types
{
    public class PlayerDashBarSetup : BarSetupBase
    {
        [SerializeField] private Image _foreground;
        [SerializeField] private Transform _barTransform;

        private IPlayerInformationProvider _playerInformationProvider;
        private FillingBarSettings _settings;
        private IPlayerInitializationStatus _playerInitializationStatus;
        private IBarView _view;

        [Inject]
        private void Construct(IPlayerInformationProvider playerInformationProvider, FillingBarSettings settings,
            IPlayerInitializationStatus playerInitializationStatus)
        {
            _playerInformationProvider = playerInformationProvider;
            _settings = settings;
            _playerInitializationStatus = playerInitializationStatus;
        }

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization =>
            new List<IInitializable> {_playerInitializationStatus};

        protected override void Prepare()
        {
            base.Prepare();
            _view = new FillingBarView(_foreground, _barTransform, _settings);
        }

        protected override void Initialize()
        {
            var model = new PlayerDashBarModel(_playerInformationProvider);
            AddDisableable(model);
            InitializePresenter(model, _view);
        }
    }
}