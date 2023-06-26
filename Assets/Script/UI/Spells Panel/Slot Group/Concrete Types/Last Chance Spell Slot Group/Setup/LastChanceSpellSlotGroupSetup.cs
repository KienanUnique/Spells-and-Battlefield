using System.Collections.Generic;
using Common.Collection_With_Reaction_On_Change;
using Settings;
using Settings.UI.Spell_Panel;
using Spells.Implementations_Interfaces.Implementations;
using Spells.Spell;
using UI.Spells_Panel.Slot.Presenter;
using UI.Spells_Panel.Slot_Group.Base.Setup;
using UI.Spells_Panel.Slot_Group.Concrete_Types.Last_Chance_Spell_Slot_Group.Model;
using UI.Spells_Panel.Slot_Group.Concrete_Types.Last_Chance_Spell_Slot_Group.Presenter;
using UI.Spells_Panel.Slot_Group.Concrete_Types.Last_Chance_Spell_Slot_Group.View;
using UI.Spells_Panel.Slot_Information;
using UnityEngine;
using Zenject;

namespace UI.Spells_Panel.Slot_Group.Concrete_Types.Last_Chance_Spell_Slot_Group.Setup
{
    public class LastChanceSpellSlotGroupSetup : SpellSlotGroupSetupBase<ILastChanceSpellGroupModelWithDisabling,
        ILastChanceSpellSlotGroupView, IIInitializableLastChanceSpellSlotGroupPresenter>
    {
        private SpellTypesSetting _spellTypesSetting;

        [Inject]
        private void Construct(SpellTypesSetting spellTypesSetting)
        {
            _spellTypesSetting = spellTypesSetting;
        }

        protected override ISpellType TypeToRepresent => _spellTypesSetting.LastChanceSpellType;

        protected override ILastChanceSpellGroupModelWithDisabling CreateModel(
            IEnumerable<ISlotInformation> slotsInformation, IEnumerable<SpellSlotPresenter> slots,
            IReadonlyListWithReactionOnChange<ISpell> spellsGroupToRepresent)
        {
            return new LastChanceSpellGroupModel(slotsInformation, slots, spellsGroupToRepresent, TypeToRepresent);
        }

        protected override ILastChanceSpellSlotGroupView CreateView(RectTransform rectTransform,
            SpellGroupSection settings, int count)
        {
            return new LastChanceSpellSlotGroupView(rectTransform, settings);
        }
    }
}