using Interfaces;

namespace Enemies.Target_Selector_From_Triggers
{
    public interface IEnemyTargetFromTriggersSelector : IReadonlyEnemyTargetFromTriggersSelector
    {
    }

    public interface IReadonlyEnemyTargetFromTriggersSelector
    {
        public delegate void CurrentTargetChangedEventHandler(IEnemyTarget oldTarget, IEnemyTarget newTarget);

        public event CurrentTargetChangedEventHandler CurrentTargetChanged;

        public IEnemyTarget CurrentTarget { get; }
    }
}