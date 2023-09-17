using Common.Readonly_Transform;
using TMPro;
using UI.Concrete_Scenes.In_Game.Popup_Text.Settings;
using UnityEngine;

namespace UI.Concrete_Scenes.In_Game.Popup_Text
{
    public interface IInitializablePopupTextController
    {
        void Initialize(TMP_Text textComponent, Transform mainTransform, IPopupTextSettings settings,
            IReadonlyTransform cameraTransform);
    }
}