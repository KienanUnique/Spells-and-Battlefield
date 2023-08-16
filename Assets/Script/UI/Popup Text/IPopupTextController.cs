using Common.Abstract_Bases.Factories;
using UnityEngine;

namespace UI.Popup_Text
{
    public interface IPopupTextController : IObjectPoolItem
    {
        public void Popup(string textToShow, Vector3 startPosition);
    }
}