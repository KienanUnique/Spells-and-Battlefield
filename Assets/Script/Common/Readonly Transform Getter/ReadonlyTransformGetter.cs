using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Common.Readonly_Transform;

namespace Common.Readonly_Transform_Getter
{
    public class ReadonlyTransformGetter : InitializableMonoBehaviourBase, IReadonlyTransformGetter
    {
        public IReadonlyTransform ReadonlyTransform { private set; get; }

        protected override void SubscribeOnEvents()
        {
        }

        protected override void UnsubscribeFromEvents()
        {
        }

        protected override void Awake()
        {
            base.Awake();
            ReadonlyTransform = new ReadonlyTransform(transform);
            SetInitializedStatus();
        }
    }
}