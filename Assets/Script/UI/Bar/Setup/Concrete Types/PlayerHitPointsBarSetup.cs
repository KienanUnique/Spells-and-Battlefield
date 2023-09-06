using System.Collections.Generic;
using Interfaces;
using Player;
using UI.Bar.Model.Concrete_Types;
using UI.Bar.View;
using UI.Bar.View.Concrete_Types.Bar_View_With_Additional_Display_Of_Changes;
using UI.Bar.View.Concrete_Types.Bar_View_With_Additional_Display_Of_Changes.Settings;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using IInitializable = Common.Abstract_Bases.Initializable_MonoBehaviour.IInitializable;

namespace UI.Bar.Setup.Concrete_Types
{
    public class PlayerHitPointsBarSetup : BarSetupBase
    {
        [SerializeField] private Image _foreground;
        [SerializeField] private Image _foregroundBackground;
        private IPlayerInformationProvider _playerInformationProvider;
        private IPlayerInitializationStatus _playerInitializationStatus;
        private IBarViewWithAdditionalDisplayOfChangesSettings _settings;
        private IBarView _view;

        [Inject]
        private void GetDependencies(IPlayerInformationProvider playerInformationProvider,
            IBarViewWithAdditionalDisplayOfChangesSettings settings,
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
            var model = new HitPointsBarModel(_playerInformationProvider);
            AddDisableable(model);
            InitializePresenter(model, _view);
        }

        protected override void Prepare()
        {
            base.Prepare();
            _view = new BarViewWithAdditionalDisplayOfChanges(_foreground, _foregroundBackground, _settings);
        }
    }
}