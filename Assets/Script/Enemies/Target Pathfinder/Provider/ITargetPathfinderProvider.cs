using Enemies.Target_Pathfinder.Setup_Data;

namespace Enemies.Target_Pathfinder.Provider
{
    public interface ITargetPathfinderProvider
    {
        ITargetPathfinder GetImplementationObject(ITargetPathfinderSetupData setupData);
    }
}