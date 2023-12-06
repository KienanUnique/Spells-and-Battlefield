using System;
using Common.Abstract_Bases.Spells_Manager;
using Enemies.Look_Point_Calculator;
using Enemies.State_Machine.States.Concrete_Types.Use_Spells.Spell_Selectors;

namespace Enemies.State_Machine.States.Concrete_Types.Use_Spells.Spells_Manager
{
    public interface IEnemySpellsManager : ISpellManager, IReadonlySpellSelector
    {
        public event Action<ILookPointCalculator> NeedChangeLookPointCalculator;
        public event Action FinishedCasting;
        public bool IsBusy { get; }
    }
}