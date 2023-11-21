namespace Spells.Spell_Handlers.Continuous
{
    public interface IContinuousSpellHandlerImplementation : IContinuousSpellHandler, ISpellsHandlerImplementationBase
    {
        public float RatioOfCompletion { get; }
    }
}