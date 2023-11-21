using Common.Abstract_Bases.Spells_Manager;
using Player.Spell_Manager.Spells_Selector;

namespace Player.Spell_Manager
{
    public interface IPlayerSpellsManager : ISpellManager, IPlayerSpellsManagerInformation, IPlayerSpellsSelector
    {
    }
}