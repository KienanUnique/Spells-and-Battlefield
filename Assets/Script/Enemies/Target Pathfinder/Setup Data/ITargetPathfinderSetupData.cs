using Common.Interfaces;
using Common.Readonly_Transform;

namespace Enemies.Target_Pathfinder.Setup_Data
{
    public interface ITargetPathfinderSetupData
    {
        IReadonlyTransform ThisPosition { get; }
        ICoroutineStarter CoroutineStarter { get; }
    }
}