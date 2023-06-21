using UnityEngine;

namespace Settings.UI
{
    [CreateAssetMenu(fileName = "Spell Panel Settings",
        menuName = ScriptableObjectsMenuDirectories.ConcreteUISettingsDirectory + "Spell Panel Settings", order = 0)]
    public class SpellPanelSettings : ScriptableObject
    {
        [SerializeField] private Sprite _emptySlotIcon;
        [SerializeField] private Vector3 _selectedGroupLocalScale;

        public Vector3 SelectedGroupLocalScale => _selectedGroupLocalScale;
        public Vector3 UnselectedGroupLocalScale => Vector3.one;
        public Sprite EmptySlotIcon => _emptySlotIcon;
    }
}