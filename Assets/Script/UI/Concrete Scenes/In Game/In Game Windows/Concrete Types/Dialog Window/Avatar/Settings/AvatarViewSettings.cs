using UI.Element.View.Settings;
using UnityEngine;

namespace UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Dialog_Window.Avatar.Settings
{
    [CreateAssetMenu(fileName = "Avatar View Settings",
        menuName = ScriptableObjectsMenuDirectories.ConcreteUISettingsDirectory + "Avatar View Settings", order = 0)]
    public class AvatarViewSettings : DefaultUIElementViewSettings, IAvatarViewSettings
    {
    }
}