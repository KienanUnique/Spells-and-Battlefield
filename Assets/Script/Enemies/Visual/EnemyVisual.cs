using Common.Abstract_Bases.Visual;
using Common.Abstract_Bases.Visual.Settings;
using Enemies.Visual.Dissolve_Effect_Controller;
using UI.Concrete_Scenes.In_Game.Enemy_Information_Panel.Presenter;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Enemies.Visual
{
    public class EnemyVisual : VisualBase, IEnemyVisual
    {
        private static readonly int IsRunningBoolHash = Animator.StringToHash("Is Running");
        private static readonly int DieTriggerHash = Animator.StringToHash("Die");
        private readonly IDissolveEffectController _dissolveEffectController;
        private readonly IEnemyInformationPanelPresenter _informationPanel;

        public EnemyVisual(RigBuilder rigBuilder, Animator characterAnimator,
            AnimatorOverrideController baseAnimatorOverrideController, IVisualSettings settings,
            IDissolveEffectController dissolveEffectController, IEnemyInformationPanelPresenter informationPanel) :
            base(rigBuilder, characterAnimator)
        {
            _characterAnimator.runtimeAnimatorController = baseAnimatorOverrideController;
            OverrideController = baseAnimatorOverrideController;
            _dissolveEffectController = dissolveEffectController;
            _informationPanel = informationPanel;
            Settings = settings;
        }

        protected override IVisualSettings Settings { get; }
        protected override AnimatorOverrideController OverrideController { get; }

        public void UpdateMovingData(bool isRunning)
        {
            _characterAnimator.SetBool(IsRunningBoolHash, isRunning);
        }

        public void PlayDieAnimation()
        {
            _rigBuilder.enabled = false;
            _characterAnimator.SetTrigger(DieTriggerHash);
            _dissolveEffectController.Disappear();
            _informationPanel.Disappear();
        }
    }
}