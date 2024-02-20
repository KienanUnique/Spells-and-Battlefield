using System;
using Common.Id_Holder;
using Systems.Dialog.Avatar_Storage;
using Systems.Dialog.Provider;
using UI.Managers.Concrete_Types.In_Game;
using UI.Window.Model;
using UnityEngine;
using Yarn.Unity;

namespace UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Dialog_Window.Model
{
    public class DialogWindowModel : UIWindowModelWithManagerBase, IDialogWindowModel
    {
        private readonly DialogueRunner _dialogRunner;
        private readonly IAvatarStorage _avatarStorage;
        private IDialogProvider _dialogToOpen;
        private string _currentAvatar;

        public DialogWindowModel(IIdHolder idHolder, IUIWindowManager manager, DialogueRunner dialogRunner,
            IAvatarStorage avatarStorage) : base(idHolder, manager)
        {
            _dialogRunner = dialogRunner;
            _avatarStorage = avatarStorage;
        }
        
        public event Action<Sprite> NeedChangeAvatar;
        public event Action NeedResetAvatar;

        public override bool CanBeClosedByPlayer => true;

        public override void Appear()
        {
            NeedResetAvatar?.Invoke();
            _currentAvatar = string.Empty;
            _dialogRunner.StartDialogue(_dialogToOpen.StartNode);
            base.Appear();
        }

        public override void Disappear()
        {
            base.Disappear();

            if (!_dialogRunner.IsDialogueRunning)
            {
                return;
            }

            _dialogRunner.Stop();
        }

        public void SetDialog(IDialogProvider dialog)
        {
            _dialogToOpen = dialog;
        }

        public void TryChangeAvatar(string nextAvatarName)
        {
            if (_currentAvatar.Equals(nextAvatarName))
            {
                return;
            }

            _currentAvatar = nextAvatarName;
            var nextAvatar = _avatarStorage.GetAvatarByName(nextAvatarName);
            NeedChangeAvatar?.Invoke(nextAvatar);
        }

        public void OnDialogueComplete()
        {
            Manager.TryCloseCurrentWindow();
        }
    }
}