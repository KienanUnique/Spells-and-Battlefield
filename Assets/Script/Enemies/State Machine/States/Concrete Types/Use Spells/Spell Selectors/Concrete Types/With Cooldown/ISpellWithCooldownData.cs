using Spells.Spell;

namespace Enemies.State_Machine.States.Concrete_Types.Use_Spells.Spell_Selectors.Concrete_Types.With_Cooldown
{
    public interface ISpellWithCooldownData
    {
        float CooldownSeconds { get; }
        ISpell Spell { get; }
    }
}