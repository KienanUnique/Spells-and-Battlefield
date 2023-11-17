using Common.Abstract_Bases.Visual;
using Common.Animation_Data;
using Enemies.Visual.Dissolve_Effect_Controller;
using UI.Concrete_Scenes.In_Game.Enemy_Information_Panel.Presenter;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Enemies.Visual
{
    public class EnemyVisual : VisualBase, IEnemyVisual
    {
        private static readonly int ActionTriggerHash = Animator.StringToHash("Do Action");
        private static readonly int ActionFloatAnimationSpeedHash = Animator.StringToHash("Use Action Speed");
        private static readonly int IsRunningBoolHash = Animator.StringToHash("Is Running");
        private static readonly int DieTriggerHash = Animator.StringToHash("Die");
        private readonly AnimatorOverrideController _baseAnimatorOverrideController;
        private readonly AnimationClip _emptyActionAnimationClip;
        private readonly IDissolveEffectController _dissolveEffectController;
        private readonly IEnemyInformationPanelPresenter _informationPanel;

        public EnemyVisual(RigBuilder rigBuilder, Animator characterAnimator,
            AnimatorOverrideController baseAnimatorOverrideController, AnimationClip emptyActionAnimationClip,
            IDissolveEffectController dissolveEffectController, IEnemyInformationPanelPresenter informationPanel) :
            base(rigBuilder, characterAnimator)
        {
            _baseAnimatorOverrideController = baseAnimatorOverrideController;
            _emptyActionAnimationClip = emptyActionAnimationClip;
            _dissolveEffectController = dissolveEffectController;
            _informationPanel = informationPanel;
            ApplyRuntimeAnimatorController(_baseAnimatorOverrideController);
        }

        public void PlayActionAnimation(IAnimationData animationData)
        {
            ApplyAnimationOverride(_baseAnimatorOverrideController,
                new AnimationOverride(_emptyActionAnimationClip, animationData.Clip));

            _characterAnimator.SetFloat(ActionFloatAnimationSpeedHash, animationData.AnimationSpeed);
            _characterAnimator.SetTrigger(ActionTriggerHash);
        }

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

        private void ApplyRuntimeAnimatorController(RuntimeAnimatorController animatorOverrideController)
        {
            _characterAnimator.runtimeAnimatorController = animatorOverrideController;
        }
    }
}