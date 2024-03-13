using Common.Interfaces;
using Common.Mechanic_Effects.Concrete_Types.Summon;
using Common.Readonly_Transform;
using Factions;
using UnityEngine;

namespace Enemies.Character.Concrete_Types.Default_Character
{
    public class EnemyDefaultCharacter : EnemyCharacterBase
    {
        public EnemyDefaultCharacter(ICoroutineStarter coroutineStarter,
            EnemyCharacterSettingsSection characterSettings, IReadonlyTransform thisTransform,
            GameObject gameObjectToLink, IFaction startFaction, ISummoner summoner = null) : base(coroutineStarter,
            characterSettings, thisTransform, gameObjectToLink, startFaction, summoner)
        {
        }
    }
}