using UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Dialog_Window.Avatar;
using UI.Element.View.Settings;
using UI.Window.View;
using UnityEngine;

namespace UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Dialog_Window.View
{
    public class DialogWindowView : DefaultUIWindowView, IDialogWindowView
    {
        private readonly IAvatarView _avatarView;

        public DialogWindowView(Transform cachedTransform, IDefaultUIElementViewSettings settings,
            IAvatarView avatarView) : base(cachedTransform, settings)
        {
            _avatarView = avatarView;
        }

        public void ChangeAvatar(Sprite newSprite)
        {
            _avatarView.ChangeAvatar(newSprite);
        }

        public void ResetAvatar()
        {
            _avatarView.ResetAvatar();
        }
    }
}