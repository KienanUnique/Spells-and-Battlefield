using UnityEngine;

namespace UI.Concrete_Scenes.In_Game.Popup_Text.Data_For_Activation
{
    public class PopupTextControllerDataForActivation : IPopupTextControllerDataForActivation
    {
        public PopupTextControllerDataForActivation(string textToShow, Vector3 spawnPosition, Quaternion spawnRotation)
        {
            TextToShow = textToShow;
            SpawnPosition = spawnPosition;
            SpawnRotation = spawnRotation;
        }

        public string TextToShow { get; }
        public Vector3 SpawnPosition { get; }
        public Quaternion SpawnRotation { get; }
    }
}