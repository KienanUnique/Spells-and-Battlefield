using Spells.Implementations_Interfaces.Implementations;
using Spells.Spell.Interfaces;

namespace Spells.Spell
{
    public class Spell : ISpell
    {
        public Spell(ISpellCardInformation spellCardInformation, ISpellAnimationInformation spellAnimationInformation,
            ISpellDataForSpellController spellDataForSpellController, ISpellPrefabProvider spellPrefabProvider)
        {
            SpellCardInformation = spellCardInformation;
            SpellAnimationInformation = spellAnimationInformation;
            SpellDataForSpellController = spellDataForSpellController;
            SpellPrefabProvider = spellPrefabProvider;
        }

        public ISpellCardInformation SpellCardInformation { get; }
        public ISpellAnimationInformation SpellAnimationInformation { get; }
        public ISpellDataForSpellController SpellDataForSpellController { get; }
        public ISpellPrefabProvider SpellPrefabProvider { get; }
    }
}