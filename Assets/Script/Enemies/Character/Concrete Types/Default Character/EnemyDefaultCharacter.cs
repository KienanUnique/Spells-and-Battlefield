using Common.Interfaces;
using Common.Mechanic_Effects.Concrete_Types.Summon;

namespace Enemies.Character.Concrete_Types.Default_Character
{
    public class EnemyDefaultCharacter : EnemyCharacterBase
    {
        public EnemyDefaultCharacter(ICoroutineStarter setupData, EnemyCharacterSettingsSection characterSettings,
            ISummoner summoner = null) : base(setupData, characterSettings, summoner)
        {
        }
    }
}