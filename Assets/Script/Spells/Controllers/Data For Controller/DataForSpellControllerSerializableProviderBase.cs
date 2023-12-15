using System;
using System.Collections.Generic;
using Enemies.Look_Point_Calculator;
using Spells.Abstract_Types.Scriptable_Objects.Parts;
using Spells.Implementations_Interfaces.Implementations;
using UnityEngine;

namespace Spells.Controllers.Data_For_Controller
{
    [Serializable]
    public abstract class DataForSpellControllerSerializableProviderBase<TIDataForController>
        where TIDataForController : IDataForSpellController
    {
        [SerializeField] private List<SpellApplierScriptableObject> _appliers;

        protected List<ISpellApplier> SpellAppliers
        {
            get
            {
                var iSpellAppliersList = new List<ISpellApplier>();
                _appliers.ForEach(spellApplier => iSpellAppliersList.Add(spellApplier.GetImplementationObject()));
                return iSpellAppliersList;
            }
        }

        public abstract TIDataForController GetImplementationObject(ISpellType type, ISpellMovement movement,
            ILookPointCalculator lookPointCalculator);
    }
}