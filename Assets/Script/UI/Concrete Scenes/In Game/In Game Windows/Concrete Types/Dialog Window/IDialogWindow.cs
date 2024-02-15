using Systems.Dialog.Provider;
using UI.Window;

namespace UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Dialog_Window
{
    public interface IDialogWindow : IUIWindow
    {
        public void SetDialog(IDialogProvider dialog);
    }
}