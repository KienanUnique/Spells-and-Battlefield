using System.Collections.Generic;
using System.Linq;
using Common.Abstract_Bases.Disableable;
using Player.Spell_Manager;
using Spells.Implementations_Interfaces.Implementations;
using UI.Concrete_Scenes.In_Game.Spells_Panel.Slot_Group;

namespace UI.Concrete_Scenes.In_Game.Spells_Panel.Panel.Model
{
    public class SpellPanelModel : BaseWithDisabling
    {
        private readonly IPlayerSpellsManagerInformation _managerInformation;
        private readonly Dictionary<ISpellType, ISpellSlotGroup> _spellsTypeGroups;

        public SpellPanelModel(List<ISpellSlotGroup> spellGroups, IPlayerSpellsManagerInformation managerInformation)
        {
            _managerInformation = managerInformation;

            _spellsTypeGroups = new Dictionary<ISpellType, ISpellSlotGroup>();
            foreach (ISpellSlotGroup group in spellGroups)
            {
                _spellsTypeGroups.Add(group.Type, group);
            }

            SelectSpellTypeGroup(_managerInformation.SelectedSpellType);
        }

        protected override void SubscribeOnEvents()
        {
            _managerInformation.SelectedSpellTypeChanged += SelectSpellTypeGroup;
            _managerInformation.TryingToUseEmptySpellTypeGroup += OnTryingToUseEmptySpellTypeGroup;
        }

        protected override void UnsubscribeFromEvents()
        {
            _managerInformation.SelectedSpellTypeChanged -= SelectSpellTypeGroup;
            _managerInformation.TryingToUseEmptySpellTypeGroup -= OnTryingToUseEmptySpellTypeGroup;
        }

        private void OnTryingToUseEmptySpellTypeGroup(ISpellType spellType)
        {
            _spellsTypeGroups[spellType].PlayAnimationOnTryingToUseEmptySpellTypeGroup();
        }

        private void SelectSpellTypeGroup(ISpellType newSelectedSpellType)
        {
            foreach (ISpellSlotGroup group in _spellsTypeGroups.Values.Where(group => group.IsSelected))
            {
                group.Unselect();
            }

            _spellsTypeGroups[newSelectedSpellType].Select();
        }
    }
}