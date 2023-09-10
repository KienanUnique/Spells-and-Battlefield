using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Common;
using Common.Abstract_Bases.Disableable;
using Common.Animation_Data;
using Common.Collection_With_Reaction_On_Change;
using Common.Readonly_Transform;
using Interfaces;
using ModestTree;
using Spells.Factory;
using Spells.Implementations_Interfaces.Implementations;
using Spells.Spell;
using Spells.Spell_Types_Settings;
using UnityEngine;

namespace Player.Spell_Manager
{
    public class PlayerSpellsManager : BaseWithDisabling, IPlayerSpellsManager
    {
        private readonly ISpellType _lastChanceSpellType;
        private readonly ICaster _player;
        private readonly ValueWithReactionOnChange<ISpellType> _selectedSpellType;
        private readonly ISpellObjectsFactory _spellObjectsFactory;
        private readonly IReadonlyTransform _spellSpawnObject;
        private readonly Dictionary<ISpellType, ListWithReactionOnChange<ISpell>> _spellsStorage;
        private bool _isWaitingForAnimationEnd;
        private IList<ISpell> _spellGroupFromWhichToCreateSpell;
        private ISpell _spellToCreate;
        private ISpellType _typeOfSpellToCreate;

        public PlayerSpellsManager(List<ISpell> startTestSpells, IReadonlyTransform spellSpawnObject, ICaster player,
            ISpellObjectsFactory spellObjectsFactory, ISpellTypesSetting spellTypesSetting)
        {
            _spellSpawnObject = spellSpawnObject;
            _player = player;
            _spellObjectsFactory = spellObjectsFactory;
            _spellsStorage = new Dictionary<ISpellType, ListWithReactionOnChange<ISpell>>();
            foreach (ISpellType type in spellTypesSetting.TypesListInOrder)
            {
                _spellsStorage.Add(type, new ListWithReactionOnChange<ISpell>());
            }

            Spells = new ReadOnlyDictionary<ISpellType, IReadonlyListWithReactionOnChange<ISpell>>(
                _spellsStorage.ToDictionary(keyValuePair => keyValuePair.Key,
                    keyValuePair => (IReadonlyListWithReactionOnChange<ISpell>) keyValuePair.Value));

            startTestSpells.ForEach(AddSpell);
            _selectedSpellType = new ValueWithReactionOnChange<ISpellType>(spellTypesSetting.TypesListInOrder[0]);
            _lastChanceSpellType = spellTypesSetting.LastChanceSpellType;
        }

        public event Action<IAnimationData> NeedPlaySpellAnimation;
        public event Action<ISpellType> TryingToUseEmptySpellTypeGroup;
        public event Action<ISpellType> SelectedSpellTypeChanged;

        public ISpellType SelectedType => _selectedSpellType.Value;
        public ReadOnlyDictionary<ISpellType, IReadonlyListWithReactionOnChange<ISpell>> Spells { get; }

        private ListWithReactionOnChange<ISpell> SelectedSpellGroup => _spellsStorage[_selectedSpellType.Value];
        private ISpell SelectedSpell => SelectedSpellGroup[0];

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

        public void TryCastSelectedSpell()
        {
            if (SelectedSpellGroup.IsEmpty())
            {
                TryingToUseEmptySpellTypeGroup?.Invoke(_selectedSpellType.Value);
            }
            else if (!_isWaitingForAnimationEnd)
            {
                _isWaitingForAnimationEnd = true;
                _typeOfSpellToCreate = _selectedSpellType.Value;
                _spellGroupFromWhichToCreateSpell = SelectedSpellGroup;
                _spellToCreate = SelectedSpell;
                NeedPlaySpellAnimation?.Invoke(_spellToCreate.SpellAnimationData);
            }
        }

        public void CreateSelectedSpell(Vector3 aimPoint)
        {
            _spellObjectsFactory.Create(_spellToCreate.SpellDataForSpellController, _spellToCreate.SpellPrefabProvider,
                _player, _spellSpawnObject.Position, Quaternion.LookRotation(aimPoint - _spellSpawnObject.Position));
            if (!Equals(_typeOfSpellToCreate, _lastChanceSpellType))
            {
                _spellGroupFromWhichToCreateSpell.RemoveAt(0);
            }
        }

        public void HandleAnimationEnd()
        {
            _isWaitingForAnimationEnd = false;
        }

        public void SelectSpellType(ISpellType typeToSelect)
        {
            _selectedSpellType.Value = typeToSelect;
        }

        public void AddSpell(ISpell newSpell)
        {
            AddSpell(newSpell.SpellType, newSpell);
        }

        protected sealed override void SubscribeOnEvents()
        {
            _selectedSpellType.AfterValueChanged += OnSelectedSpellTypeChanged;
        }

        protected override void UnsubscribeFromEvents()
        {
            _selectedSpellType.AfterValueChanged -= OnSelectedSpellTypeChanged;
        }

        private void OnSelectedSpellTypeChanged(ISpellType selectedSpellType)
        {
            SelectedSpellTypeChanged?.Invoke(_selectedSpellType.Value);
        }
    }

    public class UnrecognizedSpellTypeException : Exception
    {
        public UnrecognizedSpellTypeException() : base("Unrecognized Spell Type Exception")
        {
        }
    }
}