using System.Collections.Generic;
using System.Linq;
using Common.Abstract_Bases.Disableable;
using Player.Spell_Manager;
using Spells.Implementations_Interfaces.Implementations;
using UI.Spells_Panel.Slot_Group;

namespace UI.Spells_Panel.Panel.Model
{
    public class SpellPanelModel : BaseWithDisabling
    {
        private readonly IPlayerSpellsManagerInformation _managerInformation;
        private readonly Dictionary<ISpellType, ISpellSlotGroup> _spellsTypeGroups;

        public SpellPanelModel(List<ISpellSlotGroup> spellGroups,
            IPlayerSpellsManagerInformation managerInformation)
        {
            _managerInformation = managerInformation;

            _spellsTypeGroups = new Dictionary<ISpellType, ISpellSlotGroup>();
            foreach (var group in spellGroups)
            {
                _spellsTypeGroups.Add(group.Type, group);
            }

            SelectSpellTypeGroup(_managerInformation.SelectedType);
        }

        protected override void SubscribeOnEvents()
        {
            _managerInformation.SelectedSpellTypeChanged += SelectSpellTypeGroup;
        }

        protected override void UnsubscribeFromEvents()
        {
            _managerInformation.SelectedSpellTypeChanged -= SelectSpellTypeGroup;
        }

        private void SelectSpellTypeGroup(ISpellType newSelectedSpellType)
        {
            foreach (var group in _spellsTypeGroups.Values.Where(group => group.IsSelected))
            {
                group.Unselect();
            }

            _spellsTypeGroups[newSelectedSpellType].Select();
        }
    }
}