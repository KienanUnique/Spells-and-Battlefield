using Enemies.Movement.Setup_Data;
using UnityEngine;

namespace Enemies.Movement.Provider
{
    public abstract class EnemyMovementProviderBase : ScriptableObject, IEnemyMovementProvider
    {
        public abstract IDisableableEnemyMovement GetImplementationObject(IEnemyMovementSetupData setupData);
    }
}