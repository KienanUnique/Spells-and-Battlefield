using Spells.Implementations_Interfaces.Implementations;
using Spells.Spell.Interfaces;

namespace Spells.Spell
{
    public class Spell : ISpell
    {
        public Spell(ISpellCardInformation spellCardInformation, ISpellAnimationInformation spellAnimationInformation,
            ISpellDataForSpellController spellDataForSpellController, ISpellGameObjectProvider spellGameObjectProvider)
        {
            SpellCardInformation = spellCardInformation;
            SpellAnimationInformation = spellAnimationInformation;
            SpellDataForSpellController = spellDataForSpellController;
            SpellGameObjectProvider = spellGameObjectProvider;
        }

        public ISpellCardInformation SpellCardInformation { get; }
        public ISpellAnimationInformation SpellAnimationInformation { get; }
        public ISpellDataForSpellController SpellDataForSpellController { get; }
        public ISpellGameObjectProvider SpellGameObjectProvider { get; }
    }
}