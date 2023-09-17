using Common.Abstract_Bases.Factories.Object_Pool;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using UI.Concrete_Scenes.In_Game.Popup_Text.Data_For_Activation;

namespace UI.Concrete_Scenes.In_Game.Popup_Text
{
    public interface IPopupTextController : IObjectPoolItem<IPopupTextControllerDataForActivation>, IInitializable
    {
    }
}