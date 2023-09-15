using Interfaces;

namespace Enemies.Character.Provider
{
    public interface IEnemyCharacterProvider
    {
        public IDisableableEnemyCharacter GetImplementationObject(ICoroutineStarter coroutineStarter, ISummoner summoner = null);
    }
}