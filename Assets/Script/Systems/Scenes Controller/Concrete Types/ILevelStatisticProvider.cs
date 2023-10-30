using Systems.Score;

namespace Systems.Scenes_Controller.Concrete_Types
{
    public interface ILevelStatisticProvider
    {
        public IGameLevelStatistic Statistic { get; }
    }
}