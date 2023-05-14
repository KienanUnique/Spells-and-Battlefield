using Spells.Implementations_Interfaces.Implementations;
using Spells.Spell.Interfaces;

namespace Spells.Spell
{
    public interface ISpell
    {
        public ISpellCardInformation SpellCardInformation { get; }
        public ISpellAnimationInformation SpellAnimationInformation { get; }
        public ISpellDataForSpellController SpellDataForSpellController { get; }
        public ISpellGameObjectProvider SpellGameObjectProvider { get; }
    }
}