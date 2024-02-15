using UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Dialog_Window.Model;
using UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Dialog_Window.View;
using Yarn.Unity;

namespace UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Dialog_Window.Presenter
{
    public interface IInitializableDialogWindowPresenter
    {
        void Initialize(IDialogWindowModel model, IDialogWindowView view, DialogueRunner dialogueRunner);
    }
}