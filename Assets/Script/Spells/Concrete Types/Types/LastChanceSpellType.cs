using System;
using Spells.Abstract_Types.Implementation_Bases.Implementations;
using UnityEngine;

namespace Spells.Concrete_Types.Types
{
    [Serializable]
    public class LastChanceSpellType : SpellTypeImplementationBase
    {
        private const int LastChanceTypeID = -1;
        [SerializeField] private Color _lastChanceVisualisationColor = Color.clear;
        public override int TypeID => LastChanceTypeID;
        public override Color VisualisationColor => _lastChanceVisualisationColor;
    }
}