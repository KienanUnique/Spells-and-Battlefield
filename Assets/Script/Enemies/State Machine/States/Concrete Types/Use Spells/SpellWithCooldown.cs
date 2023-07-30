using System;
using System.Collections;
using Interfaces;
using Spells.Spell;
using Spells.Spell.Scriptable_Objects;
using UnityEngine;

namespace Enemies.State_Machine.States.Concrete_Types.Use_Spells
{
    [Serializable]
    public class SpellWithCooldown
    {
        [SerializeField] private SpellScriptableObject _spell;
        [SerializeField] private float _cooldownSeconds;

        private ICoroutineStarter _coroutineStarter;

        public bool CanUse { get; private set; } = true;

        public event Action CanUseAgain;

        public void SetCoroutineStarter(ICoroutineStarter coroutineStarter) => _coroutineStarter = coroutineStarter;

        public ISpell GetSpellAndStartCooldownTimer()
        {
            if (!CanUse)
            {
                throw new DisabledSpellUsageException();
            }

            _coroutineStarter.StartCoroutine(DisableSpellForCooldown());
            return _spell;
        }

        private IEnumerator DisableSpellForCooldown()
        {
            CanUse = false;
            yield return new WaitForSeconds(_cooldownSeconds);
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