using System;
using System.Collections.Generic;
using Puzzles.Mechanisms;

namespace Puzzles.Mechanisms_Triggers.Concrete_Types.Trigger_On_Mechanism_Job
{
    public class TriggerOnMechanismJobController : MechanismsTriggerBase, IInitializableTriggerOnMechanismJobController
    {
        private MechanismJobEventType _eventType;
        private List<IMechanismController> _mechanismController;

        public void Initialize(MechanismJobEventType eventType, List<IMechanismController> mechanismController,
            MechanismsTriggerBaseSetupData setupData)
        {
            _eventType = eventType;
            _mechanismController = mechanismController;
            InitializeBase(setupData);
        }

        protected override void SubscribeOnEvents()
        {
            switch (_eventType)
            {
                case MechanismJobEventType.Started:
                    foreach (var mechanismController in _mechanismController)
                    {
                        mechanismController.JobStarted += TryInvokeTriggerEvent;
                    }

                    break;
                case MechanismJobEventType.Finished:
                    foreach (var mechanismController in _mechanismController)
                    {
                        mechanismController.JobEnded += TryInvokeTriggerEvent;
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected override void UnsubscribeFromEvents()
        {
            switch (_eventType)
            {
                case MechanismJobEventType.Started:
                    foreach (var mechanismController in _mechanismController)
                    {
                        mechanismController.JobStarted -= TryInvokeTriggerEvent;
                    }

                    break;
                case MechanismJobEventType.Finished:
                    foreach (var mechanismController in _mechanismController)
                    {
                        mechanismController.JobEnded -= TryInvokeTriggerEvent;
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}