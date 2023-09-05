using UI.Spells_Panel.Settings.Sections.Group;
using UI.Spells_Panel.Settings.Sections.Slot;
using UnityEngine;

namespace UI.Spells_Panel.Settings
{
    [CreateAssetMenu(fileName = "Spell Panel Settings",
        menuName = ScriptableObjectsMenuDirectories.ConcreteUISettingsDirectory + "Spell Panel Settings", order = 0)]
    public class SpellPanelSettings : ScriptableObject, ISpellPanelSettings
    {
        [SerializeField] private SpellSlotSection _slotSection;
        [SerializeField] private SpellGroupSection _groupSection;

        public ISpellGroupSection GroupSection => _groupSection;
        public ISpellSlotSection SlotSection => _slotSection;
    }
}