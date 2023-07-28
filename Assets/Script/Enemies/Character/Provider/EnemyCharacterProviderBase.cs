using Interfaces;
using UnityEngine;

namespace Enemies.Character.Provider
{
    public abstract class EnemyCharacterProviderBase : ScriptableObject, IEnemyCharacterProviderBase
    {
        public abstract IDisableableEnemyCharacter GetImplementationObject(ICoroutineStarter coroutineStarter);
    }
}