using Common.Abstract_Bases.Factories.Position_Data_For_Instantiation;

namespace UI.Concrete_Scenes.In_Game.Popup_Text.Data_For_Activation
{
    public interface IPopupTextControllerDataForActivation : IPositionDataForInstantiation
    {
        public string TextToShow { get; }
    }
}