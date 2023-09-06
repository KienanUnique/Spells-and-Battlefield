using UnityEngine;

namespace UI.Bar.View.Concrete_Types.Filling_Bar.Settings
{
    [CreateAssetMenu(fileName = "Filling Bar View Settings",
        menuName = ScriptableObjectsMenuDirectories.ConcreteUISettingsDirectory + "Filling Bar View Settings",
        order = 0)]
    public class FillingBarSettings : ScriptableObject, IFillingBarSettings
    {
        [SerializeField] private float _onFillAnimationDurationSeconds = 0.3f;
        [SerializeField] private float _onFillAnimationPunchStrength = 0.15f;

        public float OnFillAnimationPunchStrength => _onFillAnimationPunchStrength;

        public float OnFillAnimationDurationSeconds => _onFillAnimationDurationSeconds;
    }
}