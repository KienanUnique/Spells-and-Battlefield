using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Common;
using Common.Readonly_Transform;
using Interfaces;
using ModestTree;
using Settings;
using Spells.Factory;
using Spells.Implementations_Interfaces.Implementations;
using Spells.Spell;
using Spells.Spell.Interfaces;
using UnityEngine;

namespace Player.Spell_Manager
{
    public class PlayerSpellsManager : BaseWithDisabling, IPlayerSpellsManager
    {
        private readonly IReadonlyTransform _spellSpawnObject;
        private readonly ICaster _player;
        private readonly ISpellObjectsFactory _spellObjectsFactory;
        private readonly Dictionary<ISpellType, List<ISpell>> _spellsStorage;
        private readonly ValueWithReactionOnChange<ISpellType> _selectedSpellType;
        private readonly ISpellType _lastChanceSpellType;
        private bool _isWaitingForAnimationFinish = false;

        public PlayerSpellsManager(List<ISpell> startTestSpells, IReadonlyTransform spellSpawnObject,
            ICaster player, ISpellObjectsFactory spellObjectsFactory, SpellTypesSetting spellTypesSetting)
        {
            _spellSpawnObject = spellSpawnObject;
            _player = player;
            _spellObjectsFactory = spellObjectsFactory;
            _spellsStorage = new Dictionary<ISpellType, List<ISpell>>();
            foreach (var type in spellTypesSetting.TypesListInOrder)
            {
                _spellsStorage.Add(type, new List<ISpell>());
            }

            startTestSpells.ForEach(AddSpell);
            _selectedSpellType = new ValueWithReactionOnChange<ISpellType>(spellTypesSetting.TypesListInOrder[0]);
            _lastChanceSpellType = spellTypesSetting.LastChanceSpellType;

            SubscribeOnEvents();
        }

        public event Action<ISpellAnimationInformation> NeedPlaySpellAnimation;
        public event Action<ISpellType> SpellUsed;
        public event Action<ISpellType> SpellTypeSlotsIsEmpty;
        public event Action<ISpellType> SelectedSpellTypeChanged;
        public event Action<ISpellType> NewSpellAdded;

        public ISpellType SelectedType => _selectedSpellType.Value;

        public ReadOnlyDictionary<ISpellType, ReadOnlyCollection<ISpell>> Spells =>
            new ReadOnlyDictionary<ISpellType, ReadOnlyCollection<ISpell>>(
                _spellsStorage.ToDictionary(kv => kv.Key, kv => new ReadOnlyCollection<ISpell>(kv.Value)));

        private List<ISpell> SelectedSpellGroup => _spellsStorage[_selectedSpellType.Value];
        private ISpell SelectedSpell => SelectedSpellGroup[0];

        public void TryCastSelectedSpell()
        {
            if (SelectedSpellGroup.IsEmpty())
            {
                SpellTypeSlotsIsEmpty?.Invoke(_selectedSpellType.Value);
            }
            else if(!_isWaitingForAnimationFinish)
            {
                NeedPlaySpellAnimation?.Invoke(SelectedSpell.SpellAnimationInformation);
                _isWaitingForAnimationFinish = true;
            }
        }

        public void CreateSelectedSpell(Quaternion direction)
        {
            SpellUsed?.Invoke(SelectedSpell.SpellType);
            _spellObjectsFactory.Create(SelectedSpell.SpellDataForSpellController,
                SelectedSpell.SpellPrefabProvider, _player, _spellSpawnObject.Position, direction);
            if (!Equals(_selectedSpellType.Value, _lastChanceSpellType))
            {
                SelectedSpellGroup.RemoveAt(0);
            }

            _isWaitingForAnimationFinish = false;
        }

        public void SelectSpellType(ISpellType typeToSelect)
        {
            _selectedSpellType.Value = typeToSelect;
        }

        public void AddSpell(ISpellType spellType, ISpell newSpell)
        {
            if (_spellsStorage.ContainsKey(spellType))
            {
                _spellsStorage[spellType].Add(newSpell);
            }
            else
            {
                _spellsStorage.Add(spellType, new List<ISpell>
                {
                    newSpell
                });
            }

            NewSpellAdded?.Invoke(spellType);
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
}