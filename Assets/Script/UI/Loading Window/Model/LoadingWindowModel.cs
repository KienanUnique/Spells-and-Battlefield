using Interfaces;
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