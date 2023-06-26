using System;
using System.Collections.Generic;
using System.Linq;
using Common.Collection_With_Reaction_On_Change;
using ModestTree;
using Spells.Implementations_Interfaces.Implementations;
using Spells.Spell;
using UI.Spells_Panel.Slot;
using UI.Spells_Panel.Slot_Group.Base.Model;
using UI.Spells_Panel.Slot_Information;

namespace UI.Spells_Panel.Slot_Group.Concrete_Types.Default_Spell_Slot_Group.Model
{
    public class DefaultSpellSlotGroupModel : SpellSlotGroupModelBaseBase, IDefaultSpellSlotGroupModelWithDisabling
    {
        public DefaultSpellSlotGroupModel(IEnumerable<ISlotInformation> slotsInformation,
            IEnumerable<ISpellSlot> slotControllers, IReadonlyListWithReactionOnChange<ISpell> spellGroupToRepresent,
            ISpellType spellTypeToRepresent) : base(slotsInformation, slotControllers, spellGroupToRepresent,
            spellTypeToRepresent)
        {
        }

        public event Action<int> SpellsCountChanged;

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

        private void OnItemsCleared(EventArgs args)
        {
            DisappearAllSlotsAndShowEmpty();
        }
    }
}