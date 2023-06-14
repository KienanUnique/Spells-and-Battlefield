using Spells.Abstract_Types.Implementation_Bases.Implementations;
using UnityEngine;

namespace Spells.Concrete_Types.Types
{
    public class LastChanceSpellType : SpellTypeImplementationBase
    {
        private const int LastChanceTypeID = -1;
        private readonly Color _lastChanceVisualisationColor = Color.clear;
        public override int TypeID => LastChanceTypeID;
        public override Color VisualisationColor => _lastChanceVisualisationColor;
    }
}