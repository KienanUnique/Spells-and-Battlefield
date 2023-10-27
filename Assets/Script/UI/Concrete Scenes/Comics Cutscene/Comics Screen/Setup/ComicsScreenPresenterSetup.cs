using System.Collections.Generic;
using Common.Abstract_Bases;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using UI.Concrete_Scenes.Comics_Cutscene.Comics_Panel.Presenter;
using UI.Concrete_Scenes.Comics_Cutscene.Comics_Screen.Model;
using UnityEngine;

namespace UI.Concrete_Scenes.Comics_Cutscene.Comics_Screen.Setup
{
    public class ComicsScreenPresenterSetup : SetupMonoBehaviourBase
    {
        [SerializeField] private List<ComicsPanelPresenter> _panels;
        private IInitializableComicsScreenPresenter _presenter;
        private IComicsScreenModel _model;

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization =>
            new List<IInitializable>(_panels);

        protected override void Prepare()
        {
            _presenter = GetComponent<IInitializableComicsScreenPresenter>();
        }

        protected override void Initialize()
        {
            _model = new ComicsScreenModel(_panels);
            _presenter.Initialize(_model);
        }
    }
}