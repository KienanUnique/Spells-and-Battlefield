using Common.Abstract_Bases.Disableable;
using Common.Abstract_Bases.Spells_Manager;

namespace Enemies.State_Machine.States.Concrete_Types.Use_Spells.Spell_Selectors
{
    public interface IEnemySpellSelector : IEnemySpellSelectorFoSpellManager, IReadonlySpellSelector, IDisableable
    {
    }
}