using Common.Readonly_Transform;
using Settings.UI;
using TMPro;
using UnityEngine;

namespace UI.Popup_Text
{
    public interface IInitializablePopupTextController
    {
        void Initialize(TMP_Text textComponent, Transform mainTransform, PopupTextSettings settings,
            IReadonlyTransform cameraTransform);
    }
}