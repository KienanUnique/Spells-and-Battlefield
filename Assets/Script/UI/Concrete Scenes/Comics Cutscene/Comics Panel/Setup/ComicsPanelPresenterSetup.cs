using System.Collections.Generic;
using System.Linq;
using Common.Abstract_Bases;
using UI.Concrete_Scenes.Comics_Cutscene.Comics_Panel.Presenter;
using UI.Concrete_Scenes.Comics_Cutscene.Comics_Panel.Settings;
using UI.Concrete_Scenes.Comics_Cutscene.Comics_Panel.View;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using IInitializable = Common.Abstract_Bases.Initializable_MonoBehaviour.IInitializable;

namespace UI.Concrete_Scenes.Comics_Cutscene.Comics_Panel.Setup
{
    public class ComicsPanelPresenterSetup : SetupMonoBehaviourBase
    {
        [SerializeField] private AppearMoveDirection _appearMoveDirection = AppearMoveDirection.InPlace;
        [SerializeField] private RectTransform _transform;
        [SerializeField] private Image _image;
        private IInitializableComicsPanelPresenter _presenter;
        private IComicsPanelView _view;
        private IComicsPanelSettings _settings;

        [Inject]
        private void GetDependencies(IComicsPanelSettings settings)
        {
            _settings = settings;
        }

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization =>
            Enumerable.Empty<IInitializable>();

        protected override void Prepare()
        {
            _presenter = GetComponent<IInitializableComicsPanelPresenter>();
            _view = new ComicsPanelView(_appearMoveDirection, _settings, _transform, _image);
        }

        protected override void Initialize()
        {
            _presenter.Initialize(_view);
        }
    }
}