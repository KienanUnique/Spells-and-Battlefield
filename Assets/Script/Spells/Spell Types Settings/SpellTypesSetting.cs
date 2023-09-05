using System;
using System.Collections.Generic;
using Spells.Abstract_Types.Scriptable_Objects.Parts;
using Spells.Concrete_Types.Types;
using Spells.Implementations_Interfaces.Implementations;
using UnityEngine;

namespace Spells.Spell_Types_Settings
{
    [CreateAssetMenu(fileName = "Spell Types Setting",
        menuName = ScriptableObjectsMenuDirectories.GeneralSettingsDirectory + "Spell Types Setting", order = 0)]
    public class SpellTypesSetting : ScriptableObject, ISpellTypesSetting
    {
        [SerializeField] private List<SpellTypeScriptableObject> _typesOrder;
        [SerializeField] private LastChanceSpellPlace _lastChanceSpellPlace;
        [SerializeField] private LastChanceSpellType _lastChanceSpellType;

        private List<SpellTypeScriptableObject> _cachedTypesOrderScriptableObjects;
        private IReadOnlyList<ISpellType> _cachedTypesInOrder;

        private enum LastChanceSpellPlace
        {
            InBeginning,
            InEnd
        }

        public IReadOnlyList<ISpellType> TypesListInOrder
        {
            get
            {
                if (_cachedTypesInOrder != null && _cachedTypesOrderScriptableObjects != null &&
                    _cachedTypesOrderScriptableObjects.Equals(_typesOrder))
                {
                    return _cachedTypesInOrder;
                }

                var resultTypesOrder = new List<ISpellType>();
                _typesOrder.ForEach(typeScriptableObject =>
                    resultTypesOrder.Add(typeScriptableObject.GetImplementationObject()));
                var lastChanceSpellTypeInsertIndex = _lastChanceSpellPlace switch
                {
                    LastChanceSpellPlace.InBeginning => 0,
                    LastChanceSpellPlace.InEnd => resultTypesOrder.Count,
                    _ => throw new ArgumentOutOfRangeException()
                };
                resultTypesOrder.Insert(lastChanceSpellTypeInsertIndex, LastChanceSpellType);
                _cachedTypesInOrder = resultTypesOrder;
                _cachedTypesOrderScriptableObjects = new List<SpellTypeScriptableObject>(_typesOrder);
                return resultTypesOrder;
            }
        }

        public ISpellType LastChanceSpellType => _lastChanceSpellType;
    }
}