using Common;
using UnityEngine;

namespace UI.Concrete_Scenes.In_Game.Popup_Text.Factory
{
    public interface IPopupHitPointsChangeTextFactory
    {
        void Create(TypeOfHitPointsChange typeOfHitPointsChange, int countOfHitPoints, Vector3 startPosition);
    }
}