using Common.Readonly_Rigidbody;
using Enemies.Movement.Enemy_Data_For_Moving;

namespace Enemies.Movement
{
    public interface IEnemyMovementForStateMachine
    {
        public IReadonlyRigidbody ReadonlyRigidbody { get; }
        public void StartKeepingCurrentTargetOnDistance(IEnemyDataForMoving dataForMoving);
        public void StopMoving();
    }
}