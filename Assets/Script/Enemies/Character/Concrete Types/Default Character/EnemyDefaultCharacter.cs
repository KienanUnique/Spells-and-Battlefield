using Common.Interfaces;
using Common.Mechanic_Effects.Concrete_Types.Summon;
using Common.Readonly_Transform;
using UnityEngine;

namespace Enemies.Character.Concrete_Types.Default_Character
{
    public class EnemyDefaultCharacter : EnemyCharacterBase
    {
        public EnemyDefaultCharacter(ICoroutineStarter setupData, EnemyCharacterSettingsSection characterSettings,
            IReadonlyTransform thisTransform, GameObject gameObjectToLink, ISummoner summoner = null) : base(setupData,
            characterSettings, thisTransform, gameObjectToLink, summoner)
        {
        }
    }
}