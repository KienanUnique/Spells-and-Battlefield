using System.Collections.Generic;
using Common.Abstract_Bases.Disableable;
using UI.Concrete_Scenes.In_Game.Spells_Panel.Slot_Group.Base.Model;
using UI.Concrete_Scenes.In_Game.Spells_Panel.Slot_Group.Base.View;

namespace UI.Concrete_Scenes.In_Game.Spells_Panel.Slot_Group.Base.Setup
{
    public interface IInitializableSpellSlotGroupPresenter<in TModel, in TView>
        where TModel : ISpellSlotGroupModelBase where TView : ISpellSlotGroupViewBase
    {
        public void Initialize(TModel model, TView view, List<IDisableable> itemsNeedDisabling);
    }
}