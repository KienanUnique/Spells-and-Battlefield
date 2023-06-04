using Spells.Spell;
using Spells.Spell.Interfaces;
using UnityEngine;

namespace Player.Spell_Manager
{
    public interface IPlayerSpellsManager
    {
        bool IsSpellSelected { get; }
        ISpell SelectedSpell { get; }
        ISpellAnimationInformation SelectedSpellAnimationInformation { get; }
        void UseSelectedSpell(Quaternion direction);
        void AddSpell(ISpell newSpell);
    }
}