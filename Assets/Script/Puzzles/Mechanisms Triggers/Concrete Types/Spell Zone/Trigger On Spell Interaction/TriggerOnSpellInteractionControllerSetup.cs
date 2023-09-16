using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Abstract_Bases;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Common.Id_Holder;
using UnityEngine;

namespace Puzzles.Mechanisms_Triggers.Concrete_Types.Spell_Zone.Trigger_On_Spell_Interaction
{
    [RequireComponent(typeof(IdHolder))]
    public class TriggerOnSpellInteractionControllerSetup : SetupMonoBehaviourBase
    {
        private IInitializableTriggerOnSpellInteractionController _controller;
        private IIdHolder _idHolder;

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization =>
            Enumerable.Empty<IInitializable>();

        protected override void Initialize()
        {
            _controller.Initialize(_idHolder);
        }

        protected override void Prepare()
        {
            _idHolder = GetComponent<IdHolder>();
            _controller = GetComponent<IInitializableTriggerOnSpellInteractionController>();
        }
    }
}