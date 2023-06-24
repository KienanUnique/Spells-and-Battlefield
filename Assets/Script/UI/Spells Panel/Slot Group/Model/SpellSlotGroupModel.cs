using System;
using System.Collections.Generic;
using System.Linq;
using Common.Abstract_Bases.Disableable;
using Common.Collection_With_Reaction_On_Change;
using ModestTree;
using Spells.Implementations_Interfaces.Implementations;
using Spells.Spell;
using UI.Spells_Panel.Slot;
using UI.Spells_Panel.Slot_Information;
using UnityEngine;

namespace UI.Spells_Panel.Slot_Group.Model
{
    public class SpellSlotGroupModel : BaseWithDisabling, ISpellSlotGroupModel
    {
        private readonly IReadonlyListWithReactionOnChange<ISpell> _spellGroupToRepresent;
        private readonly List<ObjectWithUsageFlag<ISpellSlot>> _slotObjects;
        private readonly SortedSet<ObjectWithUsageFlag<ISlotInformation>> _slots;

        // TODO: Add methods to check if slot is with empty image
        public SpellSlotGroupModel(IEnumerable<ISlotInformation> slotsInformation,
            IEnumerable<ISpellSlot> slotControllers, IReadonlyListWithReactionOnChange<ISpell> spellGroupToRepresent,
            ISpellType spellTypeToRepresent)
        {
            _spellGroupToRepresent = spellGroupToRepresent;
            _slots = new SortedSet<ObjectWithUsageFlag<ISlotInformation>>(new SortedSlotInformationComparer());
            _slotObjects = new List<ObjectWithUsageFlag<ISpellSlot>>();
            IsSelected = false;
            Type = spellTypeToRepresent;

            foreach (var slot in slotsInformation)
            {
                _slots.Add(new ObjectWithUsageFlag<ISlotInformation>(slot, false));
            }

            foreach (var slotObject in slotControllers)
            {
                _slotObjects.Add(new ObjectWithUsageFlag<ISpellSlot>(slotObject, false));
            }

            if (spellGroupToRepresent.IsEmpty())
            {
                DisappearAllSlotsAndShowEmpty();
            }
            else
            {
                // var countOfSpellsToShow = Math.Min(_slotObjects.Count, spellGroupToRepresent.Count);
                // for (var spellIndex = 0; spellIndex < countOfSpellsToShow; spellIndex++)
                // {
                //     var spell = spellGroupToRepresent[spellIndex];
                //     AppearSlot(spell);
                // }
                FillEmptySlots();
            }
        }

        public event Action<int> SpellsCountChanged;
        public bool IsSelected { get; private set; }
        public ISpellType Type { get; }

        public void Select()
        {
            if (IsSelected)
            {
                throw new InvalidOperationException("Group is already selected");
            }

            IsSelected = true;
        }

        public void Unselect()
        {
            if (!IsSelected)
            {
                throw new InvalidOperationException("Group is already unselected");
            }

            IsSelected = false;
        }

        protected sealed override void SubscribeOnEvents()
        {
            _spellGroupToRepresent.ItemAdded += OnItemAdded;
            _spellGroupToRepresent.ItemRemoved += OnItemRemoved;
            _spellGroupToRepresent.ItemReplaced += OnItemReplaced;
            _spellGroupToRepresent.ItemInserted += OnItemInserted;
            _spellGroupToRepresent.ItemsCleared += OnItemsCleared;

            _spellGroupToRepresent.ItemAdded += OnItemsCountChanged;
            _spellGroupToRepresent.ItemRemoved += OnItemsCountChanged;
            _spellGroupToRepresent.ItemReplaced += OnItemsCountChanged;
            _spellGroupToRepresent.ItemInserted += OnItemsCountChanged;
            _spellGroupToRepresent.ItemsCleared += OnItemsCountChanged;
        }

        protected override void UnsubscribeFromEvents()
        {
            _spellGroupToRepresent.ItemAdded -= OnItemAdded;
            _spellGroupToRepresent.ItemRemoved -= OnItemRemoved;
            _spellGroupToRepresent.ItemReplaced -= OnItemReplaced;
            _spellGroupToRepresent.ItemInserted -= OnItemInserted;
            _spellGroupToRepresent.ItemsCleared -= OnItemsCleared;

            _spellGroupToRepresent.ItemAdded -= OnItemsCountChanged;
            _spellGroupToRepresent.ItemRemoved -= OnItemsCountChanged;
            _spellGroupToRepresent.ItemReplaced -= OnItemsCountChanged;
            _spellGroupToRepresent.ItemInserted -= OnItemsCountChanged;
            _spellGroupToRepresent.ItemsCleared -= OnItemsCountChanged;
        }

        private void OnItemsCountChanged(EventArgs args)
        {
            SpellsCountChanged?.Invoke(_spellGroupToRepresent.Count);
        }

        private void OnItemAdded(ItemWithIndexEventArgs<ISpell> args)
        {
            if (_slots.IsEmpty() || args.Index >= _slots.Count)
            {
                return;
            }

            var frontSlotInformation = _slots.First();
            var frontSlotObject = _slotObjects.First(slotObject =>
                slotObject.StoredObject.CurrentSlotInformation == frontSlotInformation.StoredObject);
            if (frontSlotObject.StoredObject.IsEmptySlot)
            {
                frontSlotObject.StoredObject.DisappearAndForgetSpell();
                frontSlotObject.SetAsFree();
                frontSlotInformation.SetAsFree();
            }

            AppearSlot(args.Item);
        }

        private void OnItemRemoved(ItemWithIndexEventArgs<ISpell> args)
        {
            if (args.Index >= _slots.Count)
            {
                return;
            }

            var slotInformation = _slots.ElementAt(args.Index).StoredObject;
            var removedSlotObjectWithUsageFlag = _slotObjects.First(slotObject =>
                slotObject.StoredObject.CurrentSlotInformation == slotInformation);
            var removedSlotObject = removedSlotObjectWithUsageFlag.StoredObject;

            if (!removedSlotObject.CurrentSpell.Equals(args.Item))
            {
                throw new InvalidOperationException();
            }

            removedSlotObject.DisappearAndForgetSpell();
            removedSlotObjectWithUsageFlag.SetAsFree();

            if (_spellGroupToRepresent.IsEmpty())
            {
                DisappearAllSlotsAndShowEmpty();
            }
            else
            {
                for (var slotIndex = args.Index; slotIndex < _slots.Count - 1; slotIndex++)
                {
                    var nextSlot = _slots.ElementAt(slotIndex + 1);
                    var currentSlot = _slots.ElementAt(slotIndex);
                    if (nextSlot.IsUsed)
                    {
                        var nextSlotInformation = nextSlot.StoredObject;
                        var nextSpellController = FindUsedSpellObjectInSlot(nextSlotInformation).StoredObject;
                        nextSpellController.MoveToSlot(currentSlot.StoredObject);
                        currentSlot.SetAsUsed();
                        nextSlot.SetAsFree();
                    }
                    else
                    {
                        break;
                    }
                }

                FillEmptySlots();
            }
        }

        private ObjectWithUsageFlag<ISpellSlot> FindUsedSpellObjectInSlot(ISlotInformation slotInformationToSearch)
        {
            foreach (var slotObject in _slotObjects)
            {
                if (slotObject.IsUsed &&
                    slotObject.StoredObject.CurrentSlotInformation.CompareTo(slotInformationToSearch) == 0)
                {
                    return slotObject;
                }
            }

            throw new ArgumentException("Used slot object in this slot does not exist");
        }

        private void OnItemReplaced(ItemReplacedEventArgs<ISpell> args)
        {
            if (args.Index >= _slots.Count)
            {
                return;
            }

            var slotInformation = _slots.ElementAt(args.Index).StoredObject;
            var replacedSlotObjectWithUsageFlag = _slotObjects.First(slotObject =>
                slotObject.StoredObject.CurrentSlotInformation == slotInformation);
            var replacedSlotObject = replacedSlotObjectWithUsageFlag.StoredObject;
            replacedSlotObject.DisappearAndForgetSpell();
            replacedSlotObject.AppearAsSlot(slotInformation, args.NewItem);
        }

        private void OnItemInserted(ItemWithIndexEventArgs<ISpell> args)
        {
            var slotInformation = _slots.ElementAt(args.Index);
            MoveSlotsBack(slotInformation.StoredObject);
            AppearSlot(slotInformation, args.Item);
        }

        private void AppearSlot(ISpell spellToShow)
        {
            var freeSlot = _slots.First(slotObject => !slotObject.IsUsed);
            AppearSlot(freeSlot, spellToShow);
        }

        private void AppearSlot(ObjectWithUsageFlag<ISlotInformation> slotInformation, ISpell spellToShow)
        {
            var freeSlotObject = _slotObjects.First(slotObject => !slotObject.IsUsed);
            freeSlotObject.StoredObject.AppearAsSlot(slotInformation.StoredObject, spellToShow);
            freeSlotObject.SetAsUsed();
            slotInformation.SetAsUsed();
        }

        private void OnItemsCleared(EventArgs args)
        {
            DisappearAllSlotsAndShowEmpty();
        }

        private void MoveSlotsBack(ISlotInformation includedUpToSlot)
        {
            var slotsList = _slots.ToList();
            var finishItemIndex = slotsList.FindIndex(slot =>
                slot.StoredObject.CompareTo(includedUpToSlot) == 0);
            int slotIndex;
            var lastUsedSlotIndex = slotsList.FindLastIndex(slot => slot.IsUsed);
            int startIndex;
            if (lastUsedSlotIndex == _slots.Count - 1)
            {
                var lastSlotObject = _slotObjects.First(slotObject =>
                    slotObject.StoredObject.CurrentSlotInformation == _slots.Last().StoredObject);
                lastSlotObject.StoredObject.DisappearAndForgetSpell();
                startIndex = lastUsedSlotIndex - 1;
            }
            else
            {
                startIndex = lastUsedSlotIndex;
            }

            for (slotIndex = startIndex; slotIndex >= finishItemIndex; slotIndex++)
            {
                if (_slots.ElementAt(slotIndex).IsUsed)
                {
                    var currentSpellController = _slotObjects.Find(slotObject =>
                        slotObject.StoredObject.CurrentSlotInformation.CompareTo(_slots.ElementAt(slotIndex)
                            .StoredObject) == 0).StoredObject;
                    currentSpellController.MoveToSlot(_slots.ElementAt(slotIndex + 1).StoredObject);
                }
                else
                {
                    break;
                }
            }
        }

        private void FillEmptySlots()
        {
            var firstEmptySlotIndex = _slots.ToList().FindIndex(slot => !slot.IsUsed);
            for (var slotIndex = firstEmptySlotIndex; slotIndex < _slots.Count; slotIndex++)
            {
                var currentSlot = _slots.ElementAt(slotIndex);

                if (currentSlot.IsUsed)
                {
                    throw new InvalidOperationException("Trying to fill already filled slot");
                }

                if (slotIndex < _spellGroupToRepresent.Count)
                {
                    Debug.Log("AppearSlot:");
                    Debug.Log($"currentSlot.IsUsed: {currentSlot.IsUsed}");
                    Debug.Log($"currentSlot.StoredObject.LocalScale: {currentSlot.StoredObject.LocalScale}");
                    Debug.Log(
                        $"_spellGroupToRepresent[slotIndex].CardInformation.Title: {_spellGroupToRepresent[slotIndex].CardInformation.Title}");
                    AppearSlot(currentSlot, _spellGroupToRepresent[slotIndex]);
                }
                else
                {
                    break;
                }
            }
        }

        private void DisappearAllSlotsAndShowEmpty()
        {
            foreach (var slot in _slots)
            {
                slot.SetAsFree();
            }

            foreach (var slotObject in _slotObjects.Where(slotObject => slotObject.IsUsed))
            {
                slotObject.StoredObject.DisappearAndForgetSpell();
                slotObject.SetAsFree();
            }

            var slotWithUsageFlag = _slots.ElementAt(0);
            var slotObjectWithUsageFlag = _slotObjects.ElementAt(0);
            slotObjectWithUsageFlag.StoredObject.AppearAsEmptySlot(slotWithUsageFlag.StoredObject);
            slotObjectWithUsageFlag.SetAsUsed();
            slotWithUsageFlag.SetAsUsed();
        }

        private class ObjectWithUsageFlag<T>
        {
            public ObjectWithUsageFlag(T storedObject, bool isUsed)
            {
                StoredObject = storedObject;
                IsUsed = isUsed;
            }

            public bool IsUsed { get; private set; }

            public T StoredObject { get; }

            public void SetAsUsed()
            {
                IsUsed = true;
            }

            public void SetAsFree()
            {
                IsUsed = false;
            }
        }

        private class SortedSlotInformationComparer : IComparer<ObjectWithUsageFlag<ISlotInformation>>
        {
            public int Compare(ObjectWithUsageFlag<ISlotInformation> x, ObjectWithUsageFlag<ISlotInformation> y)
            {
                return y.StoredObject.CompareTo(x.StoredObject);
            }
        }
    }
}