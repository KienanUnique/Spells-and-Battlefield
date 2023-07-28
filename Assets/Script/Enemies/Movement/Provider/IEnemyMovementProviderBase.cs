using Enemies.Movement.Setup_Data;

namespace Enemies.Movement.Provider
{
    public interface IEnemyMovementProviderBase
    {
        public IDisableableEnemyMovement GetImplementationObject(IEnemyMovementSetupData setupData);
    }
}