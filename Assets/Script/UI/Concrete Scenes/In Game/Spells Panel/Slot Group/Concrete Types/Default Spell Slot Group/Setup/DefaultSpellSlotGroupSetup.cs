﻿using System.Collections.Generic;
using Common.Collection_With_Reaction_On_Change;
using Spells.Abstract_Types.Scriptable_Objects.Parts;
using Spells.Implementations_Interfaces.Implementations;
using Spells.Spell;
using TMPro;
using UI.Concrete_Scenes.In_Game.Spells_Panel.Settings.Sections.Group;
using UI.Concrete_Scenes.In_Game.Spells_Panel.Slot.Presenter;
using UI.Concrete_Scenes.In_Game.Spells_Panel.Slot_Group.Base.Setup;
using UI.Concrete_Scenes.In_Game.Spells_Panel.Slot_Group.Concrete_Types.Default_Spell_Slot_Group.Model;
using UI.Concrete_Scenes.In_Game.Spells_Panel.Slot_Group.Concrete_Types.Default_Spell_Slot_Group.Presenter;
using UI.Concrete_Scenes.In_Game.Spells_Panel.Slot_Group.Concrete_Types.Default_Spell_Slot_Group.View;
using UI.Concrete_Scenes.In_Game.Spells_Panel.Slot_Information;
using UnityEngine;

namespace UI.Concrete_Scenes.In_Game.Spells_Panel.Slot_Group.Concrete_Types.Default_Spell_Slot_Group.Setup
{
    public class DefaultSpellSlotGroupSetup : SpellSlotGroupSetupBase<IDefaultSpellSlotGroupModelWithDisabling,
        IDefaultSpellSlotGroupView, IIInitializableDefaultSpellGroupPresenter>
    {
        [SerializeField] private SpellTypeScriptableObject _typeToRepresent;
        [SerializeField] private TMP_Text _spellsCountText;

        protected override ISpellType TypeToRepresent => _typeToRepresent.GetImplementationObject();

        protected override IDefaultSpellSlotGroupModelWithDisabling CreateModel(
            IEnumerable<ISlotInformation> slotsInformation, IEnumerable<SpellSlotPresenter> slots,
            IReadonlyListWithReactionOnChange<ISpell> spellsGroupToRepresent)
        {
            return new DefaultSpellSlotGroupModel(slotsInformation, slots, spellsGroupToRepresent, TypeToRepresent);
        }

        protected override IDefaultSpellSlotGroupView CreateView(RectTransform rectTransform,
            ISpellGroupSection settings, int count)
        {
            return new DefaultSpellSlotGroupView(_spellsCountText, rectTransform, settings, count);
        }
    }
}