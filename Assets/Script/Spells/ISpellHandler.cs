using Spells.Controllers.Concrete_Types.Continuous;
using Spells.Controllers.Concrete_Types.Instant;

namespace Spells
{
    public interface ISpellHandler : IInstantSpellHandler, IContinuousSpellHandler
    {
    }

    public interface IContinuousSpellHandler
    {
        public void HandleSpell(IInformationAboutContinuousSpell informationAboutContinuousSpell);
    }

    public interface IInstantSpellHandler
    {
        public void HandleSpell(IInformationAboutInstantSpell informationAboutInstantSpell);
    }
}