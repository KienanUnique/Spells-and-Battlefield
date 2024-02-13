using System.Collections.Generic;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using UI.Concrete_Scenes.In_Game.Aim_Icon.Presenter;
using UI.Concrete_Scenes.In_Game.Continuous_Effects_Panel.Presenter;
using UI.Concrete_Scenes.In_Game.Damage_Indicator.Presenter;
using UI.Concrete_Scenes.In_Game.Gameplay_UI.Model;
using UI.Concrete_Scenes.In_Game.Gameplay_UI.Presenter;
using UI.Concrete_Scenes.In_Game.Interact_Key_Popup.Presenter;
using UI.Concrete_Scenes.In_Game.Player_Information_Panel.Presenter;
using UI.Concrete_Scenes.In_Game.Spells_Panel.Panel.Presenter;
using UI.Element;
using UI.Window.Setup;
using UnityEngine;

namespace UI.Concrete_Scenes.In_Game.Gameplay_UI.Setup
{
    public class GameplayUIPresenterSetup : WindowPresenterSetupBase
    {
        [SerializeField] private SpellPanelPresenter _spellPanel;
        [SerializeField] private PlayerInformationPanelPresenter _playerInformation;
        [SerializeField] private UIAimIconPresenter _aim;
        [SerializeField] private DamageIndicatorPresenter _damageIndicator;
        [SerializeField] private ContinuousEffectsPanelPresenter _continuousEffectsPanelPresenter;
        [SerializeField] private InteractKeyPopupPresenter _interactKeyPopupPresenter;
        private IGameplayUIModel _model;
        private IInitializableGameplayUIPresenter _presenter;

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization =>
            new List<IInitializable>(base.ObjectsToWaitBeforeInitialization)
            {
                _spellPanel,
                _playerInformation,
                _aim,
                _damageIndicator,
                _continuousEffectsPanelPresenter,
                _interactKeyPopupPresenter
            };

        protected override void Initialize()
        {
            _presenter.Initialize(_model);
        }

        protected override void Prepare()
        {
            base.Prepare();
            _model = new GameplayUIModel(IDHolder,
                new List<IUIElement>
                {
                    _spellPanel,
                    _playerInformation,
                    _aim,
                    _damageIndicator,
                    _continuousEffectsPanelPresenter,
                    _interactKeyPopupPresenter
                });
            _presenter = GetComponent<IInitializableGameplayUIPresenter>();
        }
    }
}