using UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Dialog_Window.Model;
using UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Dialog_Window.Presenter;
using UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Dialog_Window.View;
using UI.Window.Setup;
using UnityEngine;
using Yarn.Unity;

namespace UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Dialog_Window.Setup
{
    public class DialogWindowPresenterSetup : WindowPresenterSetupBase
    {
        [SerializeField] private DialogueRunner _dialogueRunner;

        private IInitializableDialogWindowPresenter _presenter;
        private IDialogWindowModel _model;
        private IDialogWindowView _view;

        protected override void Prepare()
        {
            base.Prepare();
            _presenter = GetComponent<IInitializableDialogWindowPresenter>();
            _view = new DialogWindowView(transform, DefaultUIElementViewSettings);
        }

        protected override void Initialize()
        {
            _model = new DialogWindowModel(IDHolder, Manager, _dialogueRunner);
            _presenter.Initialize(_model, _view, _dialogueRunner);
        }
    }
}