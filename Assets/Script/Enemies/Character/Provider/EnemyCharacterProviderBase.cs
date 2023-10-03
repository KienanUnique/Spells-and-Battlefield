using Common.Interfaces;
using Common.Mechanic_Effects.Concrete_Types.Summon;
using Common.Readonly_Transform;
using UnityEngine;

namespace Enemies.Character.Provider
{
    public abstract class EnemyCharacterProviderBase : ScriptableObject, IEnemyCharacterProvider
    {
        public abstract IDisableableEnemyCharacter GetImplementationObject(ICoroutineStarter coroutineStarter,
            IReadonlyTransform thisTransform, ISummoner summoner = null);
    }
}