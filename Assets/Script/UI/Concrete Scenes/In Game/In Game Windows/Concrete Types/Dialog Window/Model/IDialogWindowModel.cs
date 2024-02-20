using System;
using Systems.Dialog.Provider;
using UI.Window.Model;
using UnityEngine;

namespace UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Dialog_Window.Model
{
    public interface IDialogWindowModel : IUIWindowModel
    {
        public event Action<Sprite> NeedChangeAvatar;
        public event Action NeedResetAvatar;
        public void SetDialog(IDialogProvider dialog);
        public void TryChangeAvatar(string nextAvatarName);
        public void OnDialogueComplete();
    }
}