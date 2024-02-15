using UI.Element.View.Settings;
using UI.Window.View;
using UnityEngine;

namespace UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Dialog_Window.View
{
    public class DialogWindowView : DefaultUIWindowView, IDialogWindowView
    {
        public DialogWindowView(Transform cachedTransform, IDefaultUIElementViewSettings settings) : base(
            cachedTransform, settings)
        {
        }
    }
}