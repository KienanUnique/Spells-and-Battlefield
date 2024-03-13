using Systems.Dialog.Provider;
using UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Dialog_Window.Model;
using UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Dialog_Window.View;
using UI.Window.Model;
using UI.Window.Presenter;
using UI.Window.View;
using Yarn.Unity;

namespace UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Dialog_Window.Presenter
{
    public class DialogWindowPresenter : WindowPresenterBase, IDialogWindow, IInitializableDialogWindowPresenter
    {
        private IDialogWindowModel _model;
        private IDialogWindowView _view;
        private DialogueRunner _dialogueRunner;

        public void Initialize(IDialogWindowModel model, IDialogWindowView view, DialogueRunner dialogueRunner)
        {
            _model = model;
            _view = view;
            _dialogueRunner = dialogueRunner;
            _dialogueRunner.AddCommandHandler<string>("change_avatar", _model.TryChangeAvatar);
            SetInitializedStatus();
        }

        protected override IUIWindowModel WindowModel => _model;
        protected override IUIWindowView WindowView => _view;

        public void SetDialog(IDialogProvider dialog)
        {
            _model.SetDialog(dialog);
        }

        protected override void SubscribeOnWindowEvents()
        {
            _dialogueRunner.onDialogueComplete.AddListener(_model.OnDialogueComplete);
            _model.NeedChangeAvatar += _view.ChangeAvatar;
            _model.NeedResetAvatar += _view.ResetAvatar;
        }

        protected override void UnsubscribeFromWindowEvents()
        {
            _dialogueRunner.onDialogueComplete.RemoveListener(_model.OnDialogueComplete);
            _model.NeedChangeAvatar -= _view.ChangeAvatar;
            _model.NeedResetAvatar -= _view.ResetAvatar;
        }
    }
}