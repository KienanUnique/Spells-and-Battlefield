using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Abstract_Bases;
using Common.Abstract_Bases.Disableable;
using Common.Collection_With_Reaction_On_Change;
using ModestTree;
using Spells.Implementations_Interfaces.Implementations;
using Spells.Spell;
using UI.Spells_Panel.Slot_Controller;
using UI.Spells_Panel.Slot_Information;

namespace UI.Spells_Panel.Slot_Group.Model
{
    public class SpellSlotGroupModel : BaseWithDisabling, ISpellSlotGroupModel
    {
        private readonly IReadonlyListWithReactionOnChange<ISpell> _spellGroupToRepresent;
        private readonly List<ObjectWithUsageFlag<ISpellSlot>> _slotObjects;
        private readonly SortedSet<ObjectWithUsageFlag<ISlotInformation>> _slots;

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

            foreach (var spell in spellGroupToRepresent)
            {
                AppearSlot(spell);
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

            if (_spellGroupToRepresent.IsEmpty())
            {
                DisappearAllSlotsAndShowEmpty();
            }
            else
            {
                removedSlotObject.DisappearAndForgetSpell();
                removedSlotObjectWithUsageFlag.SetAsFree();
                var slotsList = _slots.ToList();
                var removedItemIndex = slotsList.FindIndex(slot =>
                    slot.StoredObject.CompareTo(removedSlotObject.CurrentSlotInformation) == 0);
                int slotIndex;
                for (slotIndex = removedItemIndex + 1; slotIndex < _slots.Count; slotIndex++)
                {
                    if (_slots.ElementAt(slotIndex).IsUsed)
                    {
                        var currentSpellController = _slotObjects.Find(slotObject =>
                                slotObject.StoredObject.CurrentSlotInformation.CompareTo(
                                    _slots.ElementAt(slotIndex)) == 0)
                            .StoredObject;
                        currentSpellController.MoveToSlot(_slots.ElementAt(slotIndex - 1).StoredObject);
                    }
                    else
                    {
                        break;
                    }
                }
            }
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
                            slotObject.StoredObject.CurrentSlotInformation.CompareTo(
                                _slots.ElementAt(slotIndex)) == 0)
                        .StoredObject;
                    currentSpellController.MoveToSlot(_slots.ElementAt(slotIndex + 1).StoredObject);
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
                return x.StoredObject.CompareTo(y.StoredObject);
            }
        }
    }
}