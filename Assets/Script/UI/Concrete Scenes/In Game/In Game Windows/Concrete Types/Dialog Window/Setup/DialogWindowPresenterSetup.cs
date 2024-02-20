using Systems.Dialog.Avatar_Storage;
using UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Dialog_Window.Avatar;
using UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Dialog_Window.Avatar.Settings;
using UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Dialog_Window.Model;
using UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Dialog_Window.Presenter;
using UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Dialog_Window.View;
using UI.Window.Setup;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;
using Zenject;

namespace UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Dialog_Window.Setup
{
    public class DialogWindowPresenterSetup : WindowPresenterSetupBase
    {
        [SerializeField] private DialogueRunner _dialogueRunner;
        [SerializeField] private Image _avatar;
        [SerializeField] private Transform _avatarRoot;

        private IInitializableDialogWindowPresenter _presenter;
        private IDialogWindowModel _model;
        private IDialogWindowView _view;
        private IAvatarStorage _avatarStorage;
        private IAvatarViewSettings _settings;

        [Inject]
        private void GetDependencies(IAvatarStorage avatarStorage, IAvatarViewSettings settings)
        {
            _avatarStorage = avatarStorage;
            _settings = settings;
        }

        protected override void Prepare()
        {
            base.Prepare();
            _presenter = GetComponent<IInitializableDialogWindowPresenter>();
            var avatarView = new AvatarView(_avatarRoot, _settings, _avatar);
            _view = new DialogWindowView(transform, DefaultUIElementViewSettings, avatarView);
        }

        protected override void Initialize()
        {
            _model = new DialogWindowModel(IDHolder, Manager, _dialogueRunner, _avatarStorage);
            _presenter.Initialize(_model, _view, _dialogueRunner);
        }
    }
}