using Spells.Factory;
using Spells.Spell.Interfaces;

namespace Spells.Controllers
{
    public interface IInitializableSpellObjectController
    {
        public void Initialize(ISpellDataForSpellController spellData, ICaster caster,
            ISpellObjectsFactory spellObjectsFactory);
    }
}