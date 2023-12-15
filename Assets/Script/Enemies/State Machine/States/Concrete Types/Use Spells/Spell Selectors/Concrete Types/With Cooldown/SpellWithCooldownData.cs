using System;
using Spells.Spell;
using Spells.Spell.Scriptable_Objects;
using UnityEngine;

namespace Enemies.State_Machine.States.Concrete_Types.Use_Spells.Spell_Selectors.Concrete_Types.With_Cooldown
{
    [Serializable]
    public class SpellWithCooldownData : ISpellWithCooldownData
    {
        [SerializeField] private SpellScriptableObjectBase _spell;
        [SerializeField] private float _cooldownSeconds;

        public float CooldownSeconds => _cooldownSeconds;
        public ISpell Spell => _spell;
    }
}