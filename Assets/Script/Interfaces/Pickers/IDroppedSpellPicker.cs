using Spells.Spell;

namespace Interfaces.Pickers
{
    public interface IPickableSpellPicker : IPickableItemsPicker
    {
        public void AddSpell(ISpell newSpell);
    }
}