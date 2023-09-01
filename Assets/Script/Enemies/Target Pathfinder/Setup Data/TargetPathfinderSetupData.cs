using Common.Readonly_Transform;
using Interfaces;

namespace Enemies.Target_Pathfinder.Setup_Data
{
    public class TargetPathfinderSetupData : ITargetPathfinderSetupData
    {
        public TargetPathfinderSetupData(IReadonlyTransform thisPosition, ICoroutineStarter coroutineStarter)
        {
            ThisPosition = thisPosition;
            CoroutineStarter = coroutineStarter;
        }

        public IReadonlyTransform ThisPosition { get; }
        public ICoroutineStarter CoroutineStarter { get; }
    }
}