using UnityEngine;

namespace Systems.Input_Manager.Settings
{
    [CreateAssetMenu(fileName = "Input Manager Settings",
        menuName = ScriptableObjectsMenuDirectories.SystemsSettingsDirectory + "Input Manager Settings",
        order = 0)]
    public class InputManagerSettings : ScriptableObject, IInputManagerSettings
    {
        [SerializeField] private float _inGameMouseSensitivity = 21f;

        public float InGameMouseSensitivity => _inGameMouseSensitivity;
    }
}