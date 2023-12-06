using Common.Abstract_Bases.Spells_Manager;
using Enemies.State_Machine.States.Concrete_Types.Use_Spells;

namespace Player.Spell_Manager.Spells_Selector
{
    public interface IPlayerSpellsSelectorForSpellManager : IEnemySpellSelectorFoSpellManager, IPlayerSpellsSelector
    {
    }
}