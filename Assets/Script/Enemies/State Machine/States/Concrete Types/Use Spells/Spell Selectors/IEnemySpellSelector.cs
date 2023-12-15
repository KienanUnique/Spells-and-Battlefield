using Common.Abstract_Bases.Disableable;

namespace Enemies.State_Machine.States.Concrete_Types.Use_Spells.Spell_Selectors
{
    public interface IEnemySpellSelector : ISpellSelectorFoSpellManager, IReadonlySpellSelector, IDisableable
    {
    }
}