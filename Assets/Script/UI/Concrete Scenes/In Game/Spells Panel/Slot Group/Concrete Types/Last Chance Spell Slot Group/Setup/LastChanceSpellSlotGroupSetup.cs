using System.Collections.Generic;
using Common.Collection_With_Reaction_On_Change;
using Spells.Implementations_Interfaces.Implementations;
using Spells.Spell;
using Spells.Spell_Types_Settings;
using UI.Concrete_Scenes.In_Game.Spells_Panel.Settings.Sections.Group;
using UI.Concrete_Scenes.In_Game.Spells_Panel.Slot.Presenter;
using UI.Concrete_Scenes.In_Game.Spells_Panel.Slot_Group.Base.Setup;
using UI.Concrete_Scenes.In_Game.Spells_Panel.Slot_Group.Concrete_Types.Last_Chance_Spell_Slot_Group.Model;
using UI.Concrete_Scenes.In_Game.Spells_Panel.Slot_Group.Concrete_Types.Last_Chance_Spell_Slot_Group.Presenter;
using UI.Concrete_Scenes.In_Game.Spells_Panel.Slot_Group.Concrete_Types.Last_Chance_Spell_Slot_Group.View;
using UI.Concrete_Scenes.In_Game.Spells_Panel.Slot_Information;
using UnityEngine;
using Zenject;

namespace UI.Concrete_Scenes.In_Game.Spells_Panel.Slot_Group.Concrete_Types.Last_Chance_Spell_Slot_Group.Setup
{
    public class LastChanceSpellSlotGroupSetup : SpellSlotGroupSetupBase<ILastChanceSpellGroupModelWithDisabling,
        ILastChanceSpellSlotGroupView, IIInitializableLastChanceSpellSlotGroupPresenter>
    {
        private ISpellTypesSetting _spellTypesSetting;

        [Inject]
        private void GetDependencies(ISpellTypesSetting spellTypesSetting)
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
            ISpellGroupSection settings, int count)
        {
            return new LastChanceSpellSlotGroupView(rectTransform, settings);
        }
    }
}