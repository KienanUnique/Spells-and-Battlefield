using Systems.Dialog.Provider;
using UI.Window.Model;

namespace UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Dialog_Window.Model
{
    public interface IDialogWindowModel : IUIWindowModel
    {
        public void SetDialog(IDialogProvider dialog);
        public void OnDialogueComplete();
    }
}