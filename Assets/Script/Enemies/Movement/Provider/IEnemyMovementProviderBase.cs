using Enemies.Movement.Setup_Data;

namespace Enemies.Movement.Provider
{
    public interface IEnemyMovementProvider
    {
        public IDisableableEnemyMovement GetImplementationObject(IEnemyMovementSetupData setupData);
    }
}