using System.Collections.Generic;
using Common.Abstract_Bases.Disableable;
using UI.Spells_Panel.Slot_Group.Base.Model;
using UI.Spells_Panel.Slot_Group.Base.View;

namespace UI.Spells_Panel.Slot_Group.Base.Setup
{
    public interface IInitializableSpellSlotGroupPresenter<in TModel, in TView>
        where TModel : ISpellSlotGroupModelBase where TView : ISpellSlotGroupViewBase
    {
        public void Initialize(TModel model, TView view, List<IDisableable> itemsNeedDisabling);
    }
}