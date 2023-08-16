using UnityEngine;

namespace UI.Popup_Text.Factory
{
    public interface IPopupTextFactory
    {
        IPopupTextController Create(string textToShow, Vector3 startPosition);
    }
}