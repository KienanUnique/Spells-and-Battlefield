using Common.Id_Holder;
using UI.Managers.Concrete_Types.In_Game;

namespace UI.Window.Model
{
    public abstract class UIWindowModelWithManagerBase : UIWindowModelBase
    {
        protected UIWindowModelWithManagerBase(IIdHolder idHolder, IUIWindowManager manager) : base(idHolder)
        {
            Manager = manager;
        }

        protected IUIWindowManager Manager { get; }
    }
}