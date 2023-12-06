using System;
using System.Collections;
using Common.Interfaces;
using Spells.Spell;
using UnityEngine;

namespace Enemies.State_Machine.States.Concrete_Types.Use_Spells.Spell_Selectors.Concrete_Types.With_Cooldown
{
    public class SpellWithCooldown : ISpellWithCooldown
    {
        private readonly ICoroutineStarter _coroutineStarter;
        private readonly ISpellWithCooldownData _data;

        public SpellWithCooldown(ISpellWithCooldownData data, ICoroutineStarter coroutineStarter)
        {
            _data = data;
            _coroutineStarter = coroutineStarter;
            CanUse = true;
        }

        public event Action CanUseAgain;

        public bool CanUse { get; private set; }
        public ISpell Spell => _data.Spell;

        public void StartCooldownTimer()
        {
            if (!CanUse)
            {
                throw new DisabledSpellUsageException();
            }

            _coroutineStarter.StartCoroutine(DisableSpellForCooldown());
        }

        private IEnumerator DisableSpellForCooldown()
        {
            CanUse = false;
            yield return new WaitForSeconds(_data.CooldownSeconds);
            CanUse = true;
            CanUseAgain?.Invoke();
        }
    }

    public class DisabledSpellUsageException : Exception
    {
        public DisabledSpellUsageException() : base("Trying to use disabled spell")
        {
        }
    }
}