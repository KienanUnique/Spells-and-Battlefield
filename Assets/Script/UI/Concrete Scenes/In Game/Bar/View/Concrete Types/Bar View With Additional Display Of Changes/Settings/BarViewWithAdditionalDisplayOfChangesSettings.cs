using UnityEngine;

namespace UI.Concrete_Scenes.In_Game.Bar.View.Concrete_Types.Bar_View_With_Additional_Display_Of_Changes.Settings
{
    [CreateAssetMenu(fileName = "Bar View With Additional Display Of Changes Settings",
        menuName = ScriptableObjectsMenuDirectories.ConcreteUISettingsDirectory +
                   "Bar View With Additional Display Of Changes Settings", order = 0)]
    public class BarViewWithAdditionalDisplayOfChangesSettings : ScriptableObject,
        IBarViewWithAdditionalDisplayOfChangesSettings
    {
        [SerializeField] private float _changeDuration = 0.1f;

        public float ChangeDuration => _changeDuration;
    }
}