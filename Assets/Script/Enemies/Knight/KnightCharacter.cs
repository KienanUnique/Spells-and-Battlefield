using System.Collections.Generic;
using Common.Abstract_Bases.Character;
using General_Settings_in_Scriptable_Objects;
using General_Settings_in_Scriptable_Objects.Sections;
using Interfaces;
using Settings;
using Zenject;

namespace Enemies.Knight
{
    public class KnightCharacter : CharacterBase
    {
        private KnightSettings.KnightCharacterSettingsSection _characterSettings;

        [Inject]
        private void Construct(KnightSettings knightSettings)
        {
            _characterSettings = knightSettings.KnightCharacterSettings;
        }

        protected override string NamePrefix => "Knight";
        protected override CharacterSettingsSection CharacterSettings => _characterSettings;

        public void TryDamageTargetsWithSwordAttack(List<IEnemyTarget> targets)
        {
            foreach (var target in targets)
            {
                if (target is ICharacter targetCharacter)
                {
                    targetCharacter.HandleDamage(_characterSettings.AttackSwordDamage);
                }
            }
        }
    }
}