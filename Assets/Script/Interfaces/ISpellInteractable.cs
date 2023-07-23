using Spells.Implementations_Interfaces.Implementations;

namespace Interfaces
{
    public interface ISpellInteractable : IInteractable
    {
        public void InteractAsSpellType(ISpellType spellType);
    }
}