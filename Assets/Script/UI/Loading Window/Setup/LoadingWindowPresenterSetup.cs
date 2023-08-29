﻿using System.Collections.Generic;
using System.Linq;
using UI.Loading_Window.Model;
using UI.Loading_Window.Presenter;
using UI.Loading_Window.View;
using UI.Window.Setup;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using IInitializable = Common.Abstract_Bases.Initializable_MonoBehaviour.IInitializable;

namespace UI.Loading_Window.Setup
{
    public class LoadingWindowPresenterSetup : WindowPresenterSetupBase
    {
        [SerializeField] private Image _loadingIcon;
        [SerializeField] private Transform _mainTransform;

        private ILoadingWindowModel _model;
        private ILoadingWindowView _view;
        private IInitializableLoadingWindowPresenter _presenter;
        private ILoadingWindowSettings _loadingWindowSettings;

        [Inject]
        private void Construct(ILoadingWindowSettings loadingWindowSettings)
        {
            _loadingWindowSettings = loadingWindowSettings;
        }

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization =>
            Enumerable.Empty<IInitializable>();

        protected override void Prepare()
        {
            base.Prepare();
            _presenter = GetComponent<IInitializableLoadingWindowPresenter>();
            _model = new LoadingWindowModel(_idHolder);
            _view = new LoadingWindowView(_mainTransform, _generalUIAnimationSettings, _loadingIcon.transform,
                _loadingWindowSettings);
        }

        protected override void Initialize()
        {
            _presenter.Initialize(_model, _view);
        }
    }
}