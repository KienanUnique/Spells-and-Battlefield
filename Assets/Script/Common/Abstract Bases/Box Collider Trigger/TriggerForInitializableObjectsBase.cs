using Common.Abstract_Bases.Initializable_MonoBehaviour;

namespace Common.Abstract_Bases.Box_Collider_Trigger
{
    public class TriggerForInitializableObjectsBase<TRequiredObject> : BoxColliderTriggerBase<TRequiredObject>
        where TRequiredObject : IInitializable
    {
        
    }
}