using Enemies.Movement.Setup_Data;
using UnityEngine;

namespace Enemies.Movement.Provider
{
    [CreateAssetMenu(fileName = "Enemy Movement Without Gravity Provider",
        menuName = ScriptableObjectsMenuDirectories.EnemyMovementProvidersDirectory +
                   "Enemy Movement Without Gravity Provider", order = 0)]
    public abstract class EnemyMovementProviderBase : ScriptableObject, IEnemyMovementProvider
    {
        public abstract IDisableableEnemyMovement GetImplementationObject(IEnemyMovementSetupData setupData);
    }
}