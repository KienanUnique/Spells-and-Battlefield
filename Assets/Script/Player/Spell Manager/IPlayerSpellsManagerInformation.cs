using System;
using Player.Spell_Manager.Spells_Selector;

namespace Player.Spell_Manager
{
    public interface IPlayerSpellsManagerInformation : IPlayerSpellsSelectorInformation
    {
        public event Action ContinuousSpellStarted;
        public event Action ContinuousSpellFinished;
        public float ContinuousSpellRatioOfCompletion { get; }
    }
}