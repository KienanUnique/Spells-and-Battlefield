using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Common.Id_Holder;
using UI.Concrete_Scenes.In_Game.Gameplay_UI.Model;

namespace UI.Concrete_Scenes.In_Game.Gameplay_UI.Presenter
{
    public class GameplayUIPresenter : InitializableMonoBehaviourBase, IGameplayUI, IInitializableGameplayUIPresenter
    {
        private IGameplayUIModel _model;

        public void Initialize(IGameplayUIModel model)
        {
            _model = model;
            SetInitializedStatus();
        }

        public int Id => _model.Id;
        public bool CanBeClosedByPlayer => _model.CanBeClosedByPlayer;

        public bool Equals(IIdHolder other)
        {
            return _model.Equals(other);
        }

        public void Appear()
        {
            _model.Appear();
        }

        public void Disappear()
        {
            _model.Disappear();
        }

        protected override void SubscribeOnEvents()
        {
        }

        protected override void UnsubscribeFromEvents()
        {
        }
    }
}