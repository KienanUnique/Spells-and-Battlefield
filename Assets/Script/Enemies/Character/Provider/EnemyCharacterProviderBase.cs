using Interfaces;
using UnityEngine;

namespace Enemies.Character.Provider
{
    public abstract class EnemyCharacterProviderBase : ScriptableObject, IEnemyCharacterProvider
    {
        public abstract IDisableableEnemyCharacter GetImplementationObject(ICoroutineStarter coroutineStarter,
            ISummoner summoner = null);
    }
}