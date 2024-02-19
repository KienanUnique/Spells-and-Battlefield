using UI.Element.View;
using UI.Element.View.Settings;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Dialog_Window.Avatar
{
    public class AvatarView : DefaultUIElementView, IAvatarView
    {
        private readonly Image _image;

        public AvatarView(Transform cachedTransform, IDefaultUIElementViewSettings settings, Image image) : base(
            cachedTransform, settings)
        {
            _image = image;
        }

        public void ChangeAvatar(Sprite newSprite)
        {
            Disappear();
            _image.sprite = newSprite;
            Appear();
        }
    }
}