using System.Collections.Generic;
using Player;
using UI.Concrete_Scenes.In_Game.Damage_Indicator.Model;
using UI.Concrete_Scenes.In_Game.Damage_Indicator.Settings;
using UI.Concrete_Scenes.In_Game.Damage_Indicator.View;
using UI.Concrete_Scenes.In_Game.Damage_Indicator.View.Damage_Indicator_Element;
using UI.Element.Setup;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using IInitializable = Common.Abstract_Bases.Initializable_MonoBehaviour.IInitializable;

namespace UI.Concrete_Scenes.In_Game.Damage_Indicator.Setup
{
    public class PlayerDamageIndicatorPresenterSetup : UIElementPresenterSetup
    {
        [SerializeField] private List<Image> _indicatorImagesInClockwiseOrder;
        private IInitializableDamageIndicatorPresenter _presenter;
        private IDamageIndicatorElementSettings _settings;
        private IPlayerInformationProvider _playerInformation;
        private IPlayerInitializationStatus _playerInitializationStatus;
        private IDamageIndicatorView _view;

        [Inject]
        private void GetDependencies(IDamageIndicatorElementSettings settings,
            IPlayerInitializationStatus playerInitializationStatus, IPlayerInformationProvider playerInformation)
        {
            _settings = settings;
            _playerInformation = playerInformation;
            _playerInitializationStatus = playerInitializationStatus;
        }

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization =>
            new List<IInitializable> {_playerInitializationStatus};

        protected override void Prepare()
        {
            _presenter = GetComponent<IInitializableDamageIndicatorPresenter>();

            var indicatorsInClockwiseOrder = new List<IDamageIndicatorElement>();
            foreach (Image image in _indicatorImagesInClockwiseOrder)
            {
                indicatorsInClockwiseOrder.Add(new DamageIndicatorElement(_settings, image));
            }

            _view = new DamageIndicatorView(transform, DefaultUIElementViewSettings, indicatorsInClockwiseOrder);
        }

        protected override void Initialize()
        {
            var model = new DamageIndicatorModel(_playerInformation.MainTransform);
            _presenter.Initialize(model, _view, _playerInformation);
        }
    }
}