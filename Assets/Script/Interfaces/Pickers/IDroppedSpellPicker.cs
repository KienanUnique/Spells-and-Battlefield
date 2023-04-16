using Spells;

namespace Interfaces.Pickers
{
    public interface IDroppedSpellPicker : IDroppedItemsPicker
    {
        public void AddSpell(ISpell newSpell);
    }
}