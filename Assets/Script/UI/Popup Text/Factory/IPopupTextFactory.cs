using UnityEngine;

namespace UI.Popup_Text.Factory
{
    public interface IPopupTextFactory
    {
        void Create(string textToShow, Vector3 startPosition);
    }
}