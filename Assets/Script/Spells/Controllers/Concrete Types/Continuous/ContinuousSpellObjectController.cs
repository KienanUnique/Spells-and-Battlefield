using System;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Spells.Controllers.Concrete_Types.Continuous.Data_For_Activation;
using UnityEngine;

namespace Spells.Controllers.Concrete_Types.Continuous
{
    public class ContinuousSpellObjectController :
        SpellObjectControllerBase<IDataForActivationContinuousSpellObjectController>,
        IInitializableContinuousSpellController
    {
        public void Initialize()
        {
            InitializeBase();
            SetInitializedStatus();
        }

        public event Action Finished;

        public float RatioOfCompletion =>
            Mathf.Min(TimePassedFromActivation / DataForActivation.ConcreteSpellControllerData.DurationInSeconds, 1f);

        public void Interrupt()
        {
            HandleFinishSpell();
        }

        protected override void HandleFinishSpell()
        {
            Finished?.Invoke();
            base.HandleFinishSpell();
        }

        private void Update()
        {
            if (CurrentInitializableMonoBehaviourStatus == InitializableMonoBehaviourStatus.Initialized &&
                RatioOfCompletion == 1f)
            {
                HandleFinishSpell();
            }
        }
    }
}