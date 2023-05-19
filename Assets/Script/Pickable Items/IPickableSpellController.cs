using Spells.Spell;

namespace Pickable_Items
{
    public interface IPickableSpellController : IPickableItem
    {
        public void SetStoredData(ISpell storedObject);
    }
}