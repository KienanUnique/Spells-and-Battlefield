using UI.Window;

namespace UI.Concrete_Scenes.In_Game.Bar
{
    public interface IBarController : IUIWindow
    {
        public void UpdateValue(float valueRatio);
    }
}