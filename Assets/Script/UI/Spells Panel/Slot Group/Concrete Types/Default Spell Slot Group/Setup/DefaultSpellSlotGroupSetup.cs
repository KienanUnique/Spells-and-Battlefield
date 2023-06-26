using System.Collections.Generic;
using Common.Collection_With_Reaction_On_Change;
using Settings.UI.Spell_Panel;
using Spells.Abstract_Types.Scriptable_Objects.Parts;
using Spells.Implementations_Interfaces.Implementations;
using Spells.Spell;
using TMPro;
using UI.Spells_Panel.Slot.Presenter;
using UI.Spells_Panel.Slot_Group.Base.Setup;
using UI.Spells_Panel.Slot_Group.Concrete_Types.Default_Spell_Slot_Group.Model;
using UI.Spells_Panel.Slot_Group.Concrete_Types.Default_Spell_Slot_Group.Presenter;
using UI.Spells_Panel.Slot_Group.Concrete_Types.Default_Spell_Slot_Group.View;
using UI.Spells_Panel.Slot_Information;
using UnityEngine;

namespace UI.Spells_Panel.Slot_Group.Concrete_Types.Default_Spell_Slot_Group.Setup
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
            SpellGroupSection settings, int count)
        {
            return new DefaultSpellSlotGroupView(_spellsCountText, rectTransform, settings, count);
        }
    }
}