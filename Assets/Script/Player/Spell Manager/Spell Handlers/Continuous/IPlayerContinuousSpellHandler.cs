using Spells;

namespace Player.Spell_Manager.Spell_Handlers.Continuous
{
    public interface IPlayerContinuousSpellHandler : IContinuousSpellHandler, IPlayerSpellsHandler
    {
        public float RatioOfCompletion { get; }
    }
}