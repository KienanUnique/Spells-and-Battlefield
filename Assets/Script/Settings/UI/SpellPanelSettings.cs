using UnityEngine;

namespace Settings.UI
{
    [CreateAssetMenu(fileName = "Spell Panel Settings",
        menuName = ScriptableObjectsMenuDirectories.ConcreteUISettingsDirectory + "Spell Panel Settings", order = 0)]
    public class SpellPanelSettings : ScriptableObject
    {
        [SerializeField] private Sprite _emptySlotIcon;
        [SerializeField] private Vector2 _selectedGroupSizeDelta;

        public Vector2 SelectedGroupSizeDelta => _selectedGroupSizeDelta;
        public Vector2 UnselectedGroupSizeDelta => Vector2.one;
        public Sprite EmptySlotIcon => _emptySlotIcon;
    }
}