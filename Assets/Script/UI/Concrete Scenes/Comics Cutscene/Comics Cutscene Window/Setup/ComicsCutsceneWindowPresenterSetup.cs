using System.Collections.Generic;
using Systems.Input_Manager.Concrete_Types.Comics_Cutscene;
using UI.Concrete_Scenes.Comics_Cutscene.Comics_Cutscene_Window.Model;
using UI.Concrete_Scenes.Comics_Cutscene.Comics_Data;
using UI.Concrete_Scenes.Comics_Cutscene.Comics_Screen;
using UI.Concrete_Scenes.Comics_Cutscene.Comics_Screen.Factory;
using UI.Concrete_Scenes.Comics_Cutscene.Comics_Screen.Provider;
using UI.Concrete_Scenes.Main_Menu.View.Empty;
using UI.Window.Setup;
using UnityEngine;
using Zenject;
using IInitializable = Common.Abstract_Bases.Initializable_MonoBehaviour.IInitializable;

namespace UI.Concrete_Scenes.Comics_Cutscene.Comics_Cutscene_Window.Setup
{
    public class ComicsCutsceneWindowPresenterSetup : DefaultWindowPresenterSetupBase
    {
        [SerializeField] private Transform _rootForContent;
        [SerializeField] private ComicsData _comicsData;
        private IInitializableComicsCutsceneWindowPresenter _presenter;
        private IComicsCutsceneWindowModel _model;
        private IComicsCutsceneInputManager _inputManager;
        private IComicsScreenFactory _factory;
        private List<IInitializableComicsScreen> _screens;

        [Inject]
        private void GetDependencies(IComicsCutsceneInputManager inputManager, IComicsScreenFactory factory)
        {
            _inputManager = inputManager;
            _factory = factory;
        }

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization
        {
            get
            {
                var objectsToWaitBeforeInitialization =
                    new List<IInitializable>(base.ObjectsToWaitBeforeInitialization);
                objectsToWaitBeforeInitialization.AddRange(_screens);
                return objectsToWaitBeforeInitialization;
            }
        }

        protected override void Prepare()
        {
            base.Prepare();
            _presenter = GetComponent<IInitializableComicsCutsceneWindowPresenter>();
            _screens = new List<IInitializableComicsScreen>();
            foreach (IComicsScreenProvider comicsScreenProvider in _comicsData.ScreensInOrder)
            {
                _screens.Add(_factory.Create(comicsScreenProvider, _rootForContent));
            }
        }

        protected override void Initialize()
        {
            _model = new ComicsCutsceneWindowModel(IDHolder, new List<IComicsScreen>(_screens));
            _presenter.Initialize(_model, new EmptyUIWindowView(), _inputManager);
        }
    }
}