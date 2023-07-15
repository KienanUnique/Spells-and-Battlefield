using Spells.Implementations_Interfaces.Implementations;

namespace Interfaces
{
    public interface ISpellInteractable : IIdHolder
    {
        public void InteractAsSpellType(ISpellType spellType);
    }
}