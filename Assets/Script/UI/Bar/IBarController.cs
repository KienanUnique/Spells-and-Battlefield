using UI.Window;

namespace UI.Bar
{
    public interface IBarController : IUIWindow
    {
        public void UpdateValue(float valueRatio);
    }
}