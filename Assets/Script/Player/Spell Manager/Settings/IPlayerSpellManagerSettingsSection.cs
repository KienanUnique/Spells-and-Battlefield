using Spells.Spell;

namespace Player.Spell_Manager.Settings
{
    public interface IPlayerSpellManagerSettings
    {
        ISpell LastChanceSpell { get; }
    }
}