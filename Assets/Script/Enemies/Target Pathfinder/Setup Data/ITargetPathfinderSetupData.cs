using Common.Readonly_Transform;
using Interfaces;

namespace Enemies.Target_Pathfinder.Setup_Data
{
    public interface ITargetPathfinderSetupData
    {
        IReadonlyTransform ThisPosition { get; }
        ICoroutineStarter CoroutineStarter { get; }
    }
}