using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Common;
using Common.Abstract_Bases.Disableable;
using Common.Collection_With_Reaction_On_Change;
using ModestTree;
using Spells.Implementations_Interfaces.Implementations;
using Spells.Spell;
using Spells.Spell_Types_Settings;

namespace Player.Spell_Manager.Spells_Selector
{
    public class PlayerSpellsSelectorForSpellManager : BaseWithDisabling, IPlayerSpellsSelectorForSpellManager
    {
        private readonly Dictionary<ISpellType, ListWithReactionOnChange<ISpell>> _spellsStorage = new();

        private readonly ValueWithReactionOnChange<int> _selectedSpellTypeIndex = new(0);
        private readonly ISpellType[] _spellTypesInOrder;
        private readonly ISpellType _lastChanceSpellType;
        private IList<ISpell> _spellGroupFromWhichToCreateSpell;
        private ISpellType _typeOfSpellToCreate;

        public PlayerSpellsSelectorForSpellManager(IEnumerable<ISpell> startSpells,
            ISpellTypesSetting spellTypesSetting)
        {
            _spellTypesInOrder = spellTypesSetting.TypesListInOrder.ToArray();
            foreach (var type in spellTypesSetting.TypesListInOrder)
            {
                _spellsStorage.Add(type, new ListWithReactionOnChange<ISpell>());
            }

            Spells = new ReadOnlyDictionary<ISpellType, IReadonlyListWithReactionOnChange<ISpell>>(
                _spellsStorage.ToDictionary(keyValuePair => keyValuePair.Key,
                    keyValuePair => (IReadonlyListWithReactionOnChange<ISpell>) keyValuePair.Value));

            foreach (var startSpell in startSpells)
            {
                AddSpell(startSpell);
            }

            _lastChanceSpellType = spellTypesSetting.LastChanceSpellType;
        }

        public event Action<ISpellType> SelectedSpellTypeChanged;
        public event Action<ISpellType> TryingToUseEmptySpellTypeGroup;
        public ISpellType SelectedSpellType => _spellTypesInOrder[_selectedSpellTypeIndex.Value];

        public ReadOnlyDictionary<ISpellType, IReadonlyListWithReactionOnChange<ISpell>> Spells { get; }

        public ISpell RememberedSpell { get; private set; }

        private ListWithReactionOnChange<ISpell> SelectedSpellGroup => _spellsStorage[SelectedSpellType];
        private ISpell SelectedSpell => SelectedSpellGroup.First();

        public void SelectSpellTypeWithIndex(int indexToSelect)
        {
            _selectedSpellTypeIndex.Value = indexToSelect;
        }

        public void SelectNextSpellType()
        {
            var nextIndex = _selectedSpellTypeIndex.Value + 1;
            if (nextIndex < _spellTypesInOrder.Length)
            {
                SelectSpellTypeWithIndex(nextIndex);
            }
        }

        public void SelectPreviousSpellType()
        {
            var nextIndex = _selectedSpellTypeIndex.Value - 1;
            if (nextIndex >= 0)
            {
                SelectSpellTypeWithIndex(nextIndex);
            }
        }

        public void AddSpell(ISpellType spellType, ISpell newSpell)
        {
            if (_spellsStorage.ContainsKey(spellType))
            {
                _spellsStorage[spellType].Insert(0, newSpell);
            }
            else
            {
                throw new UnrecognizedSpellTypeException();
            }
        }

        public void AddSpell(ISpell newSpell)
        {
            AddSpell(newSpell.SpellType, newSpell);
        }

        public bool TryToRememberSelectedSpellInformation()
        {
            if (SelectedSpellGroup.IsEmpty())
            {
                TryingToUseEmptySpellTypeGroup?.Invoke(SelectedSpellType);
                return false;
            }

            _typeOfSpellToCreate = SelectedSpellType;
            _spellGroupFromWhichToCreateSpell = SelectedSpellGroup;
            RememberedSpell = SelectedSpell;
            return true;
        }

        public void RemoveRememberedSpell()
        {
            if (!Equals(_typeOfSpellToCreate, _lastChanceSpellType))
            {
                _spellGroupFromWhichToCreateSpell?.RemoveAt(0);
            }

            _typeOfSpellToCreate = null;
            _spellGroupFromWhichToCreateSpell = null;
            RememberedSpell = null;
        }

        protected override void SubscribeOnEvents()
        {
            _selectedSpellTypeIndex.AfterValueChanged += OnSelectedSpellTypeIndexChanged;
        }

        protected override void UnsubscribeFromEvents()
        {
            _selectedSpellTypeIndex.AfterValueChanged -= OnSelectedSpellTypeIndexChanged;
        }

        private void OnSelectedSpellTypeIndexChanged(int obj)
        {
            SelectedSpellTypeChanged?.Invoke(SelectedSpellType);
        }
    }
}