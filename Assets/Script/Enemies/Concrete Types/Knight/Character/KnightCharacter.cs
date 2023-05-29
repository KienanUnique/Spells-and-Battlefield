using System.Collections.Generic;
using Common.Abstract_Bases.Character;
using Interfaces;
using Settings;

namespace Enemies.Concrete_Types.Knight.Character
{
    public class KnightCharacter : CharacterBase, IKnightCharacter
    {
        private const string NamePrefix = "Knight";
        private readonly KnightSettings.KnightCharacterSettingsSection _characterSettings;

        public KnightCharacter(ICoroutineStarter coroutineStarter,
            KnightSettings.KnightCharacterSettingsSection characterSettings) :
            base(coroutineStarter, characterSettings, NamePrefix)
        {
            _characterSettings = characterSettings;
        }

        public void TryDamageTargetsWithSwordAttack(List<IEnemyTarget> targets)
        {
            foreach (var target in targets)
            {
                if (target is IInteractableCharacter targetCharacter)
                {
                    targetCharacter.HandleDamage(_characterSettings.AttackSwordDamage);
                }
            }
        }
    }
}