using Common;
using UnityEngine;

namespace UI.Popup_Text.Factory
{
    public interface IPopupHitPointsChangeTextFactory
    {
        void Create(TypeOfHitPointsChange typeOfHitPointsChange, int countOfHitPoints, Vector3 startPosition);
    }
}