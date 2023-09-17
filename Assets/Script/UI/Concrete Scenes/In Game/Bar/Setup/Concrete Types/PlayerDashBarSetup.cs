using System.Collections.Generic;
using Player;
using UI.Concrete_Scenes.In_Game.Bar.Model.Concrete_Types;
using UI.Concrete_Scenes.In_Game.Bar.View;
using UI.Concrete_Scenes.In_Game.Bar.View.Concrete_Types.Filling_Bar;
using UI.Concrete_Scenes.In_Game.Bar.View.Concrete_Types.Filling_Bar.Settings;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using IInitializable = Common.Abstract_Bases.Initializable_MonoBehaviour.IInitializable;

namespace UI.Concrete_Scenes.In_Game.Bar.Setup.Concrete_Types
{
    public class PlayerDashBarSetup : BarSetupBase
    {
        [SerializeField] private Image _foreground;
        [SerializeField] private Transform _barTransform;

        private IPlayerInformationProvider _playerInformationProvider;
        private IPlayerInitializationStatus _playerInitializationStatus;
        private IFillingBarSettings _settings;
        private IBarView _view;

        [Inject]
        private void GetDependencies(IPlayerInformationProvider playerInformationProvider, IFillingBarSettings settings,
            IPlayerInitializationStatus playerInitializationStatus)
        {
            _playerInformationProvider = playerInformationProvider;
            _settings = settings;
            _playerInitializationStatus = playerInitializationStatus;
        }

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization =>
            new List<IInitializable> {_playerInitializationStatus};

        protected override void Initialize()
        {
            var model = new PlayerDashBarModel(_playerInformationProvider);
            AddDisableable(model);
            InitializePresenter(model, _view);
        }

        protected override void Prepare()
        {
            base.Prepare();
            _view = new FillingBarView(_foreground, _barTransform, _settings);
        }
    }
}