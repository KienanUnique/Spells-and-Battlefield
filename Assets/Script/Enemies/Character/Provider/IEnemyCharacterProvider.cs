using Common.Interfaces;
using Common.Mechanic_Effects.Concrete_Types.Summon;

namespace Enemies.Character.Provider
{
    public interface IEnemyCharacterProvider
    {
        public IDisableableEnemyCharacter GetImplementationObject(ICoroutineStarter coroutineStarter,
            ISummoner summoner = null);
    }
}