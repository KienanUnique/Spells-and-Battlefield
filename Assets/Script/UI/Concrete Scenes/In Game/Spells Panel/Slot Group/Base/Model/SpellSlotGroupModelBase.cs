using System;
using System.Collections.Generic;
using System.Linq;
using Common.Abstract_Bases.Disableable;
using Common.Collection_With_Reaction_On_Change;
using ModestTree;
using Spells.Implementations_Interfaces.Implementations;
using Spells.Spell;
using UI.Concrete_Scenes.In_Game.Spells_Panel.Slot;
using UI.Concrete_Scenes.In_Game.Spells_Panel.Slot_Information;

namespace UI.Concrete_Scenes.In_Game.Spells_Panel.Slot_Group.Base.Model
{
    public abstract class SpellSlotGroupModelBase : BaseWithDisabling, ISpellSlotGroupModelBaseWithDisabling
    {
        protected readonly List<ObjectWithUsageFlag<ISpellSlot>> _slotObjects;
        protected readonly SortedSet<ObjectWithUsageFlag<ISlotInformation>> _slots;
        protected readonly IReadonlyListWithReactionOnChange<ISpell> _spellGroupToRepresent;

        protected SpellSlotGroupModelBase(IEnumerable<ISlotInformation> slotsInformation,
            IEnumerable<ISpellSlot> slotControllers, IReadonlyListWithReactionOnChange<ISpell> spellGroupToRepresent,
            ISpellType spellTypeToRepresent)
        {
            _spellGroupToRepresent = spellGroupToRepresent;
            var sortedSlotInformationComparer = new SortedSlotInformationComparer();
            _slots = new SortedSet<ObjectWithUsageFlag<ISlotInformation>>(sortedSlotInformationComparer);
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
                slotObject.ChangeBackgroundColor(spellTypeToRepresent.VisualisationColor);
            }

            if (spellGroupToRepresent.IsEmpty())
            {
                DisappearAllSlotsAndShowEmpty();
            }
            else
            {
                FillEmptySlots();
            }
        }

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

        protected ObjectWithUsageFlag<ISpellSlot> FindUsedSpellObjectInSlot(ISlotInformation slotInformationToSearch)
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

        protected void AppearSlot(ISpell spellToShow)
        {
            var freeSlot = _slots.First(slotObject => !slotObject.IsUsed);
            AppearSlot(freeSlot, spellToShow);
        }

        protected void AppearSlot(ObjectWithUsageFlag<ISlotInformation> slotInformation, ISpell spellToShow)
        {
            var freeSlotObject = _slotObjects.First(slotObject => !slotObject.IsUsed);
            freeSlotObject.StoredObject.AppearAsSlot(slotInformation.StoredObject, spellToShow);
            freeSlotObject.SetAsUsed();
            slotInformation.SetAsUsed();
        }

        protected void MoveSlotsFront(int startIndex)
        {
            for (var i = _slots.Count - 1 - startIndex; i < _slots.Count; i++)
            {
                var slotToFree = _slots.ElementAt(i);
                if (!slotToFree.IsUsed)
                {
                    continue;
                }

                var lastSlotObject = FindSlotController(slotToFree.StoredObject);
                lastSlotObject.StoredObject.DisappearAndForgetSpell();
                lastSlotObject.SetAsFree();
                slotToFree.SetAsFree();
            }

            for (var i = _slots.Count - 2; i >= startIndex; i--)
            {
                var fromMoveSlot = _slots.ElementAt(i);
                if (!fromMoveSlot.IsUsed)
                {
                    continue;
                }

                var currentSpellControllerWithDisabling = FindSlotController(fromMoveSlot.StoredObject);
                var currentSpellController = currentSpellControllerWithDisabling.StoredObject;

                var nextSlotIndex = i + 1;
                var nextSlot = _slots.ElementAt(nextSlotIndex);

                currentSpellController.MoveToSlot(nextSlot.StoredObject);
                fromMoveSlot.SetAsFree();
                nextSlot.SetAsUsed();
            }
        }

        protected void FillEmptySlots()
        {
            var firstEmptySlotIndex = _slots.ToList().FindIndex(slot => !slot.IsUsed);
            for (var slotIndex = firstEmptySlotIndex; slotIndex < _slots.Count; slotIndex++)
            {
                var currentSlot = _slots.ElementAt(slotIndex);

                if (currentSlot.IsUsed)
                {
                    throw new InvalidOperationException("Trying to fill already filled slot");
                }

                if (slotIndex >= _spellGroupToRepresent.Count)
                {
                    break;
                }

                AppearSlot(currentSlot, _spellGroupToRepresent[slotIndex]);
            }
        }

        protected void DisappearAllSlotsAndShowEmpty()
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

            var slotWithUsageFlag = _slots.First();
            var slotObjectWithUsageFlag = _slotObjects.First();
            slotObjectWithUsageFlag.StoredObject.AppearAsEmptySlot(slotWithUsageFlag.StoredObject);
            slotObjectWithUsageFlag.SetAsUsed();
            slotWithUsageFlag.SetAsUsed();
        }

        private ObjectWithUsageFlag<ISpellSlot> FindSlotController(ISlotInformation spellSlot)
        {
            return _slotObjects.Find(slotObject =>
                slotObject.IsUsed && slotObject.StoredObject.CurrentSlotInformation.CompareTo(spellSlot) == 0);
        }

        protected class ObjectWithUsageFlag<T>
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

        protected class SortedSlotInformationComparer : IComparer<ObjectWithUsageFlag<ISlotInformation>>
        {
            public int Compare(ObjectWithUsageFlag<ISlotInformation> x, ObjectWithUsageFlag<ISlotInformation> y)
            {
                return y.StoredObject.CompareTo(x.StoredObject);
            }
        }
    }
}