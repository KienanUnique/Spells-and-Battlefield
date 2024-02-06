using System;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Common.Readonly_Transform;
using Spells.Controllers.Concrete_Types.Continuous.Data_For_Controller;
using Spells.Factory;
using UnityEngine;

namespace Spells.Controllers.Concrete_Types.Continuous
{
    public class ContinuousSpellObjectController : SpellObjectControllerBase, IInitializableContinuousSpellController
    {
        private IDataForContinuousSpellController _spellControllerData;

        public void Initialize(IDataForContinuousSpellController spellControllerData, ICaster caster,
            IReadonlyTransform castPoint, ISpellObjectsFactory spellObjectsFactory)
        {
            _spellControllerData = spellControllerData;
            InitializeBase(caster, spellObjectsFactory, spellControllerData, castPoint);
            SetInitializedStatus();
        }

        public event Action Finished;

        public float RatioOfCompletion =>
            Mathf.Min(TimePassedFromInitialize / _spellControllerData.DurationInSeconds, 1f);

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