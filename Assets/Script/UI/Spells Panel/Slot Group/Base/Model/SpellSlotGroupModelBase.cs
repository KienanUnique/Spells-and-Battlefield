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

namespace UI.Spells_Panel.Slot_Group.Base.Model
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
            _slots = new SortedSet<ObjectWithUsageFlag<ISlotInformation>>(new SortedSlotInformationComparer());
            _slotObjects = new List<ObjectWithUsageFlag<ISpellSlot>>();
            IsSelected = false;
            Type = spellTypeToRepresent;

            foreach (ISlotInformation slot in slotsInformation)
            {
                _slots.Add(new ObjectWithUsageFlag<ISlotInformation>(slot, false));
            }

            foreach (ISpellSlot slotObject in slotControllers)
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
            foreach (ObjectWithUsageFlag<ISpellSlot> slotObject in _slotObjects)
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
            ObjectWithUsageFlag<ISlotInformation> freeSlot = _slots.First(slotObject => !slotObject.IsUsed);
            AppearSlot(freeSlot, spellToShow);
        }

        protected void AppearSlot(ObjectWithUsageFlag<ISlotInformation> slotInformation, ISpell spellToShow)
        {
            ObjectWithUsageFlag<ISpellSlot> freeSlotObject = _slotObjects.First(slotObject => !slotObject.IsUsed);
            freeSlotObject.StoredObject.AppearAsSlot(slotInformation.StoredObject, spellToShow);
            freeSlotObject.SetAsUsed();
            slotInformation.SetAsUsed();
        }

        protected void MoveSlotsBack(ISlotInformation includedUpToSlot)
        {
            List<ObjectWithUsageFlag<ISlotInformation>> slotsList = _slots.ToList();
            int finishItemIndex = slotsList.FindIndex(slot => slot.StoredObject.CompareTo(includedUpToSlot) == 0);
            int slotIndex;
            int lastUsedSlotIndex = slotsList.FindLastIndex(slot => slot.IsUsed);
            int startIndex;
            if (lastUsedSlotIndex == _slots.Count - 1)
            {
                ObjectWithUsageFlag<ISpellSlot> lastSlotObject = _slotObjects.First(slotObject =>
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
                    ISpellSlot currentSpellController = _slotObjects.Find(slotObject =>
                                                                        slotObject.StoredObject.CurrentSlotInformation
                                                                            .CompareTo(_slots.ElementAt(slotIndex)
                                                                                .StoredObject) ==
                                                                        0)
                                                                    .StoredObject;
                    currentSpellController.MoveToSlot(_slots.ElementAt(slotIndex + 1).StoredObject);
                }
                else
                {
                    break;
                }
            }
        }

        protected void FillEmptySlots()
        {
            int firstEmptySlotIndex = _slots.ToList().FindIndex(slot => !slot.IsUsed);
            for (int slotIndex = firstEmptySlotIndex; slotIndex < _slots.Count; slotIndex++)
            {
                ObjectWithUsageFlag<ISlotInformation> currentSlot = _slots.ElementAt(slotIndex);

                if (currentSlot.IsUsed)
                {
                    throw new InvalidOperationException("Trying to fill already filled slot");
                }

                if (slotIndex < _spellGroupToRepresent.Count)
                {
                    AppearSlot(currentSlot, _spellGroupToRepresent[slotIndex]);
                }
                else
                {
                    break;
                }
            }
        }

        protected void DisappearAllSlotsAndShowEmpty()
        {
            foreach (ObjectWithUsageFlag<ISlotInformation> slot in _slots)
            {
                slot.SetAsFree();
            }

            foreach (ObjectWithUsageFlag<ISpellSlot> slotObject in _slotObjects.Where(slotObject => slotObject.IsUsed))
            {
                slotObject.StoredObject.DisappearAndForgetSpell();
                slotObject.SetAsFree();
            }

            ObjectWithUsageFlag<ISlotInformation> slotWithUsageFlag = _slots.ElementAt(0);
            ObjectWithUsageFlag<ISpellSlot> slotObjectWithUsageFlag = _slotObjects.ElementAt(0);
            slotObjectWithUsageFlag.StoredObject.AppearAsEmptySlot(slotWithUsageFlag.StoredObject);
            slotObjectWithUsageFlag.SetAsUsed();
            slotWithUsageFlag.SetAsUsed();
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