using Common.Interfaces;
using Common.Mechanic_Effects.Concrete_Types.Summon;
using Common.Readonly_Transform;
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
            IReadonlyTransform thisTransform, GameObject gameObjectToLink, ISummoner summoner = null)
        {
            return new EnemyDefaultCharacter(coroutineStarter, _settings, thisTransform, gameObjectToLink, summoner);
        }
    }
}