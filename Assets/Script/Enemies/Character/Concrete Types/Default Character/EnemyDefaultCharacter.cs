using Interfaces;

namespace Enemies.Character.Concrete_Types.Default_Character
{
    public class EnemyDefaultCharacter : EnemyCharacterBase
    {
        public EnemyDefaultCharacter(ICoroutineStarter setupData, EnemyCharacterSettingsSection characterSettings) :
            base(setupData, characterSettings)
        {
        }
    }
}