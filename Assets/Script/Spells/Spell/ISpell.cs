using Pickable_Items.Data_For_Creating;
using Spells.Implementations_Interfaces.Implementations;
using Spells.Spell.Interfaces;

namespace Spells.Spell
{
    public interface ISpell : IPickableCardDataForCreating
    {
        public ISpellAnimationInformation SpellAnimationInformation { get; }
        public ISpellDataForSpellController SpellDataForSpellController { get; }
        public ISpellPrefabProvider SpellPrefabProvider { get; }
    }
}