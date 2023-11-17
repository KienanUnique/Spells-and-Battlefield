using System.Collections.Generic;
using Enemies.Look_Point_Calculator;
using Spells.Implementations_Interfaces;
using Spells.Implementations_Interfaces.Implementations;

namespace Spells.Controllers.Data_For_Controller
{
    public interface IDataForSpellController : ISpellImplementation
    {
        public ISpellMovement SpellObjectMovement { get; }
        public ISpellType SpellType { get; }
        public IReadOnlyList<ISpellApplier> SpellAppliers { get; }
        public ILookPointCalculator LookPointCalculator { get; }
    }
}