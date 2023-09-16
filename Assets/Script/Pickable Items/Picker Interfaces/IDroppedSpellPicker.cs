using Spells.Spell;

namespace Pickable_Items.Picker_Interfaces
{
    public interface IPickableSpellPicker : IPickableItemsPicker
    {
        public void AddSpell(ISpell newSpell);
    }
}