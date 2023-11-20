using Spells.Data_For_Spell_Implementation;

namespace Spells.Implementations_Interfaces
{
    public interface ISpellImplementation
    {
        public void Initialize(IDataForSpellImplementation data);
    }
}