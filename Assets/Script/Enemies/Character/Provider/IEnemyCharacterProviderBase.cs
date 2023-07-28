using Interfaces;

namespace Enemies.Character.Provider
{
    public interface IEnemyCharacterProviderBase
    {
        public IDisableableEnemyCharacter GetImplementationObject(ICoroutineStarter coroutineStarter);
    }
}