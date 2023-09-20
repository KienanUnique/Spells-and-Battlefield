using Common.Abstract_Bases.Box_Collider_Trigger;
using Common.Abstract_Bases.Character;

namespace Common
{
    public class InstantKillZone : BoxColliderTriggerBase<ICharacter>
    {
        protected override void OnRequiredObjectEnteringDetected(ICharacter requiredObject)
        {
            requiredObject.DieInstantly();
        }
    }
}