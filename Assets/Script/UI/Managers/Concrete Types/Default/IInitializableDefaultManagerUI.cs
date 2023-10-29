using Systems.Input_Manager;
using Systems.Scenes_Controller.Concrete_Types;
using UI.Loading_Window;
using UI.Managers.UI_Windows_Stack_Manager;

namespace UI.Managers.Concrete_Types.Default
{
    public interface IInitializableDefaultManagerUI
    {
        public void Initialize(IInputManagerForUI inputManagerForUI, IUIWindowsStackManager windowsManager,
            IScenesController scenesController, ILoadingWindow loadingWindow);
    }
}