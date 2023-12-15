using UnityEngine;

namespace Common.Abstract_Bases.Visual
{
    public struct AnimationOverride
    {
        public AnimationOverride(AnimationClip originalClip, AnimationClip overrideClip)
        {
            OriginalClip = originalClip;
            OverrideClip = overrideClip;
        }

        public AnimationClip OriginalClip { get; }
        public AnimationClip OverrideClip { get; }
    }
}