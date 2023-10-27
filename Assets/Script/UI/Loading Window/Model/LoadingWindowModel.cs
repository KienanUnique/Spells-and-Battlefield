using Common.Id_Holder;
using UI.Managers.Concrete_Types.In_Game;
using UI.Window.Model;

namespace UI.Loading_Window.Model
{
    public class LoadingWindowModel : UIWindowModelBase, ILoadingWindowModel
    {
        public LoadingWindowModel(IIdHolder idHolder) : base(idHolder)
        {
        }

        public override bool CanBeClosedByPlayer => false;
    }
}