using UnityEngine;

namespace Systems.In_Game_Systems.Time_Controller.Settings
{
    [CreateAssetMenu(fileName = "Time Controller Settings",
        menuName = ScriptableObjectsMenuDirectories.SystemsSettingsDirectory + "Time Controller Settings",
        order = 0)]
    public class TimeControllerSettings : ScriptableObject, ITimeControllerSettings
    {
        [Range(0, 1f)] [SerializeField] private float _dashAimingTimeScaleRatio = 0.1f;

        public float DashAimingTimeScaleRatio => _dashAimingTimeScaleRatio;
    }
}