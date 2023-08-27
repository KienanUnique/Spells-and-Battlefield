using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Interfaces;
using UI.Gameplay_UI.Model;

namespace UI.Gameplay_UI.Presenter
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

        public void Appear()
        {
            _model.Appear();
        }

        public void Disappear()
        {
            _model.Disappear();
        }

        public bool Equals(IIdHolder other) => _model.Equals(other);

        protected override void SubscribeOnEvents()
        {
        }

        protected override void UnsubscribeFromEvents()
        {
        }
    }
}