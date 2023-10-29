using Common.Id_Holder;
using Systems.Scenes_Controller;
using UI.Managers.Concrete_Types.In_Game;

namespace UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Game_Over_Window.Model
{
    public class GameOverWindowModel : InGameWindowModelBase, IGameOverWindowModel
    {
        public GameOverWindowModel(IIdHolder idHolder, IUIWindowManager manager,
            IInGameSceneController inGameSceneController) : base(idHolder, manager, inGameSceneController)
        {
        }

        public override bool CanBeClosedByPlayer => false;
    }
}