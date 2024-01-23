using Spells.Factory.Continuous;
using Spells.Factory.Instant;

namespace Spells.Factory
{
    public interface ISpellObjectsFactory : IInstantSpellsFactory, IContinuousSpellsFactory
    {
    }
}