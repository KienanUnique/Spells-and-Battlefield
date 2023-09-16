using Common.Interfaces;
using Common.Mechanic_Effects.Concrete_Types.Summon;
using Enemies.Character.Provider;
using UnityEngine;

namespace Enemies.Character.Concrete_Types.Default_Character
{
    [CreateAssetMenu(fileName = "Enemy Default Character",
        menuName = ScriptableObjectsMenuDirectories.EnemyCharacterProvidersDirectory + "Enemy Default Character",
        order = 0)]
    public class EnemyDefaultCharacterProvider : EnemyCharacterProviderBase
    {
        [SerializeField] private EnemyCharacterSettingsSection _settings;

        public override IDisableableEnemyCharacter GetImplementationObject(ICoroutineStarter coroutineStarter,
            ISummoner summoner = null)
        {
            return new EnemyDefaultCharacter(coroutineStarter, _settings, summoner);
        }
    }
}