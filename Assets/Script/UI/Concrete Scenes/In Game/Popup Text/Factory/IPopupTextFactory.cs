using UnityEngine;

namespace UI.Concrete_Scenes.In_Game.Popup_Text.Factory
{
    public interface IPopupTextFactory
    {
        void Create(string textToShow, Vector3 startPosition);
    }
}