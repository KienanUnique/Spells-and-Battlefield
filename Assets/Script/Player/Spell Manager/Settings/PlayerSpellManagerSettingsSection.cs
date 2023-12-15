using System;
using Spells.Spell;
using Spells.Spell.Scriptable_Objects;
using UnityEngine;

namespace Player.Spell_Manager.Settings
{
    [Serializable]
    public class PlayerSpellManagerSettingsSection : IPlayerSpellManagerSettings
    {
        [SerializeField] private SpellScriptableObjectBase _lastChanceSpell;

        public ISpell LastChanceSpell => _lastChanceSpell;
    }
}