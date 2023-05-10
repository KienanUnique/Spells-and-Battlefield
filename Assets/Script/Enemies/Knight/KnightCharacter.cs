using System.Collections.Generic;
using General_Settings_in_Scriptable_Objects;
using Interfaces;
using Zenject;

namespace Enemies.Knight
{
    public class KnightCharacter : Character
    {
        private KnightSettings.KnightCharacterSettingsSection _characterSettings;

        [Inject]
        private void Construct(KnightSettings knightSettings)
        {
            _characterSettings = knightSettings.KnightCharacterSettings;
        }

        protected override string NamePrefix => "Knight";
        protected override CharacterSettingsSection CharacterSettings => _characterSettings;

        public void DamageTargetsWithSwordAttack(List<ICharacter> targets)
        {
            targets.ForEach(target => target.HandleDamage(_characterSettings.AttackSwordDamage));
        }
    }
}