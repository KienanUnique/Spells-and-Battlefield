﻿using System.Collections.Generic;
using System.Linq;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using UI.Element;
using UI.Gameplay_UI.Model;
using UI.Gameplay_UI.Presenter;
using UI.Player_Information_Panel.Presenter;
using UI.Spells_Panel.Panel.Presenter;
using UI.Window.Setup;
using UnityEngine;

namespace UI.Gameplay_UI.Setup
{
    public class GameplayUIPresenterSetup : WindowPresenterSetupBase
    {
        [SerializeField] private SpellPanelPresenter _spellPanel;
        [SerializeField] private PlayerInformationPanelPresenter _playerInformation;
        private IInitializableGameplayUIPresenter _presenter;
        private IGameplayUIModel _model;

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization =>
            new List<IInitializable> {_spellPanel, _playerInformation};

        protected override void Prepare()
        {
            base.Prepare();
            _model = new GameplayUIModel(_idHolder, new List<IUIElement> {_spellPanel, _playerInformation});
            _presenter = GetComponent<IInitializableGameplayUIPresenter>();
        }

        protected override void Initialize()
        {
            _presenter.Initialize(_model);
        }
    }
}