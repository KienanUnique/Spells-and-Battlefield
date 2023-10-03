using Common.Interfaces;
using Common.Mechanic_Effects.Concrete_Types.Summon;
using Common.Readonly_Transform;

namespace Enemies.Character.Concrete_Types.Default_Character
{
    public class EnemyDefaultCharacter : EnemyCharacterBase
    {
        public EnemyDefaultCharacter(ICoroutineStarter setupData, EnemyCharacterSettingsSection characterSettings,
            IReadonlyTransform thisTransform, ISummoner summoner = null) : base(setupData, characterSettings,
            thisTransform, summoner)
        {
        }
    }
}