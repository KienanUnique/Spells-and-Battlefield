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
        private readonly Dictionary<ISpellType, ListWithReactionOnChange<ISpell>> _spellsStorage =
            new Dictionary<ISpellType, ListWithReactionOnChange<ISpell>>();

        private readonly ValueWithReactionOnChange<int> _selectedSpellTypeIndex = new ValueWithReactionOnChange<int>(0);
        private readonly ISpellType[] _spellTypesInOrder;
        private readonly ISpellType _lastChanceSpellType;
        private ISpell _spellToCreate;
        private IList<ISpell> _spellGroupFromWhichToCreateSpell;
        private ISpellType _typeOfSpellToCreate;

        public PlayerSpellsSelectorForSpellManager(List<ISpell> startTestSpells, ISpellTypesSetting spellTypesSetting)
        {
            _spellTypesInOrder = spellTypesSetting.TypesListInOrder.ToArray();
            foreach (ISpellType type in spellTypesSetting.TypesListInOrder)
            {
                _spellsStorage.Add(type, new ListWithReactionOnChange<ISpell>());
            }

            Spells = new ReadOnlyDictionary<ISpellType, IReadonlyListWithReactionOnChange<ISpell>>(
                _spellsStorage.ToDictionary(keyValuePair => keyValuePair.Key,
                    keyValuePair => (IReadonlyListWithReactionOnChange<ISpell>) keyValuePair.Value));

            startTestSpells.ForEach(AddSpell);
            _lastChanceSpellType = spellTypesSetting.LastChanceSpellType;
        }

        public event Action<ISpellType> SelectedSpellTypeChanged;
        public event Action<ISpellType> TryingToUseEmptySpellTypeGroup;

        public ISpellType SelectedSpellType => _spellTypesInOrder[_selectedSpellTypeIndex.Value];
        private ListWithReactionOnChange<ISpell> SelectedSpellGroup => _spellsStorage[SelectedSpellType];
        public ISpell SelectedSpell => SelectedSpellGroup[0];
        public ISpell RememberedSpell => _spellToCreate;

        public ReadOnlyDictionary<ISpellType, IReadonlyListWithReactionOnChange<ISpell>> Spells { get; }

        public void SelectSpellTypeWithIndex(int indexToSelect)
        {
            _selectedSpellTypeIndex.Value = indexToSelect;
        }

        public void SelectNextSpellType()
        {
            int nextIndex = _selectedSpellTypeIndex.Value + 1;
            if (nextIndex < _spellTypesInOrder.Length)
            {
                SelectSpellTypeWithIndex(nextIndex);
            }
        }

        public void SelectPreviousSpellType()
        {
            int nextIndex = _selectedSpellTypeIndex.Value - 1;
            if (nextIndex >= 0)
            {
                SelectSpellTypeWithIndex(nextIndex);
            }
        }

        public void AddSpell(ISpellType spellType, ISpell newSpell)
        {
            if (_spellsStorage.ContainsKey(spellType))
            {
                _spellsStorage[spellType].Add(newSpell);
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
            _spellToCreate = SelectedSpell;
            return true;
        }

        public void RemoveRememberedSpell()
        {
            if (!Equals(_typeOfSpellToCreate, _lastChanceSpellType))
            {
                _spellGroupFromWhichToCreateSpell.RemoveAt(0);
            }

            _typeOfSpellToCreate = null;
            _spellGroupFromWhichToCreateSpell = null;
            _spellToCreate = null;
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