using UnityEngine;

namespace UI.Bar.View.Concrete_Types.Bar_View_With_Additional_Display_Of_Changes
{
    [CreateAssetMenu(fileName = "Bar View With Additional Display Of Changes Settings",
        menuName = ScriptableObjectsMenuDirectories.ConcreteUISettingsDirectory +
                   "Bar View With Additional Display Of Changes Settings", order = 0)]
    public class BarViewWithAdditionalDisplayOfChangesSettings : ScriptableObject
    {
        [SerializeField] private float _changeDuration = 0.1f;

        public float ChangeDuration => _changeDuration;
    }
}