using Interfaces;
using UI.Managers.Concrete_Types.In_Game;
using UI.Window.Model;

namespace UI.Loading_Window.Model
{
    public class LoadingWindowModel : UIWindowModelBase, ILoadingWindowModel
    {
        public LoadingWindowModel(IIdHolder idHolder, IUIWindowManager manager) : base(idHolder, manager)
        {
        }

        public override bool CanBeClosedByPlayer => false;
    }
}