using System.Collections.Generic;
using Common;
using Common.Abstract_Bases.Disableable;
using UI.Spells_Panel.Slot_Group.Model;
using UI.Spells_Panel.Slot_Group.View;

namespace UI.Spells_Panel.Slot_Group.Setup
{
    public interface IInitializableSpellSlotGroupPresenter
    {
        void Initialize(ISpellSlotGroupModel model, ISpellSlotGroupView view, List<IDisableable> itemsNeedDisabling);
    }
}