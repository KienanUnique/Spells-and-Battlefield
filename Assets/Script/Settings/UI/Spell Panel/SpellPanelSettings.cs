using UnityEngine;

namespace Settings.UI.Spell_Panel
{
    [CreateAssetMenu(fileName = "Spell Panel Settings",
        menuName = ScriptableObjectsMenuDirectories.ConcreteUISettingsDirectory + "Spell Panel Settings", order = 0)]
    public class SpellPanelSettings : ScriptableObject
    {
        [SerializeField] private SpellSlotSection _slotSection;
        [SerializeField] private SpellGroupSection _groupSection;

        public SpellGroupSection GroupSection => _groupSection;
        public SpellSlotSection SlotSection => _slotSection;
    }
}